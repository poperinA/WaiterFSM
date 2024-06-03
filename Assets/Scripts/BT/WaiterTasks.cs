using UnityEngine;
using TMPro;
using UnityEngine.AI;
using Panda;
using System.Collections;

public class WaiterTasks : MonoBehaviour
{
    //UI Elements
    public TextMeshProUGUI displayText;       // Text display for the waiter
    public TextMeshProUGUI displayTextPlayer; // Text display for the player

    //Player Settings
    public GameObject player;                 // Reference to the player object
    public MonoBehaviour playerMovementScript;// Script controlling player movement

    //Static Flags
    public static bool isPlayerSeated = false;
    public static bool foodEaten = false;
    public static bool foodServed = false;
    public static bool serviceButtonPressed = false;
    public static bool refillButtonPressed = false;
    public static bool leaveRestaurant = false;
    public static bool kitchenFire = false;

    //Timing Settings
    public float foodPreparationTime = 10f;   // Time to prepare food
    public float foodEatingTime = 5f;         // Time to eat food

    //Particle Effects
    public ParticleSystem cookingParticles;   // Particle system for cooking effect
    public ParticleSystem fireParticles;   // Particle system for fire effect
    public ParticleSystem extinguisherParticles;   // Particle system for fire effect

    //Dish Settings
    public GameObject dishPrefab;             // Prefab for the dish
    public GameObject emptyDishPrefab;        // Prefab for the empty dish
    public Transform botHandTransform;        // Transform for the bot's hand

    //Navigation
    public NavMeshAgent navAgent;             // NavMesh agent for movement
    private Transform target;                 // Target transform for navigation

    //Internal State
    private GameObject customer;              // Reference to the customer object
    public GameObject Dish;                   // Current dish object
    private Transform seat;                   // Reference to the seat transform
    private string currentTableTag;           // Tag for the current table
    private int partySize = 0;                // Size of the party
    private bool Stay = false;                // Flag for staying
    private bool Leave = false;               // Flag for leaving
    private bool foodReady = false;           // Flag for food readiness
    private bool takeOrder = false;
    private bool customerComplaint = false;
    private bool foodOrdered = false;



    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        cookingParticles.Stop();
        fireParticles.Stop();
        extinguisherParticles.Stop();
    }


    [Task]
    bool Display(string text)
    {
        if (displayText != null)
        {
            displayText.text = text;
            displayText.enabled = text != "";
        }
        return true;
    }


    [Task]
    public bool DisplayPlayer(string text)
    {
        if (displayTextPlayer != null)
        {
            displayTextPlayer.text = text;
            displayTextPlayer.enabled = text != "";
        }
        return true;
    }


    [Task]
    void MoveTo(string tag)
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag(tag).transform;
        }

        navAgent.stoppingDistance = 1f;

        if (navAgent.destination != target.transform.position)
        {
            navAgent.SetDestination(target.position);
        }

        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            Task.current.Succeed();
            target = null;
        }
    }


    [Task]
    bool Idle()
    {
        displayText.text = "Idling";
        return true;
    }


    [Task]
    void DetectCustomerInWaitingArea()
    {
        var task = Task.current;

        if (task.isStarting)
        {
            customer = GameObject.FindGameObjectWithTag("Customer");

            if (customer != null && Vector3.Distance(customer.transform.position, GameObject.FindGameObjectWithTag("CustomerWaitingArea").transform.position) < 3f)
            {
                task.Succeed();
            }
            else
            {
                task.Fail();
            }
        }
    }


    [Task]
    void Query()
    {
        var task = Task.current;

        if (task.isStarting)
        {
            displayText.text = "Press 1, 2, or 3 for number of people in your party.";
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            partySize = 1;
            task.Succeed();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            partySize = 2;
            task.Succeed();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            partySize = 3;
            task.Succeed();
        }
    }


    [Task]
    void GuideToTable()
    {
        var task = Task.current;
        currentTableTag = partySize <= 2 ? "Table4" : "Table3";
        string seatTag = partySize <= 2 ? "Table4Seat" : "Table3Seat";

        if (task.isStarting)
        {
            target = GameObject.FindGameObjectWithTag(currentTableTag)?.transform;
            seat = GameObject.FindGameObjectWithTag(seatTag)?.transform;

            if (target != null)
            {
                //Debug.Log("Guiding to: " + target.name);
                navAgent.stoppingDistance = 1f;
                navAgent.SetDestination(target.position);
            }
            else
            {
                Debug.LogError("Table with tag " + currentTableTag + " not found.");
                task.Fail();
            }
        }

        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            task.Succeed();
        }
    }


    [Task]
    void WaitForCustomerToSeat()
    {
        var task = Task.current;

        if (customer != null && seat != null && Vector3.Distance(customer.transform.position, seat.position) < 1f)
        {
            // Disable player movement
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false;
            }

            // Lock the player in the middle of the seat
            customer.transform.position = seat.position;

            DisplayPlayer("S - Service, R - Drink refill, L - Leave");


            isPlayerSeated = true;
            task.Succeed();
        }
    }


    [Task]
    void DetectServiceBtnPressed()
    {
        if (serviceButtonPressed && isPlayerSeated)
        {
            Task.current.Succeed();
            serviceButtonPressed = false;
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void GiveOptions()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) && !foodOrdered)
        {
            takeOrder = true;
            customerComplaint = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && foodOrdered)
        {
            DisplayPlayer("S - Service, R - Drink refill, L - Leave");
            Task.current.Fail();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            customerComplaint = true;
            takeOrder = false;
        }


        if (takeOrder || customerComplaint)
        {
            Task.current.Succeed();
        }

    }


    [Task]
    void DetectOrderTaking()
    {
        if (takeOrder && Dish == null)
        {
            Task.current.Succeed();
        }
        else if (takeOrder && Dish != null)
        {
            DisplayPlayer("S - Service, R - Drink refill, L - Leave");
            takeOrder = false;
            Task.current.Fail();
        }
        else
        {
            Task.current.Fail();
        }
    }


    [Task]
    void DetectComplaint()
    {
        if (customerComplaint)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }


    [Task]
    void OrderInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            foodOrdered = true;
            DisplayPlayer("1 - Order, 2 - Complain");
            takeOrder = false;
            serviceButtonPressed = false;
            StartCoroutine(FoodPreparationCountdown());
            Task.current.Succeed();
        }
        else
        {
            //Debug.Log("Not a valid option");
        }
    }
    private IEnumerator FoodPreparationCountdown()
    {
        // Enable the particle system
        if (cookingParticles != null)
        {
            cookingParticles.Play();
        }

        yield return new WaitForSeconds(foodPreparationTime);

        // Stop the particle system
        if (cookingParticles != null)
        {
            cookingParticles.Stop();
        }

        foodReady = true;
    }


    [Task]
    void Complain()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            customerComplaint = false;
            serviceButtonPressed = false;
            Task.current.Succeed();
        }
    }


    [Task]
    void StayOrLeave()
    {

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Stay = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Leave = true;
        }

        if (Stay)
        {
            serviceButtonPressed = false;

            Stay = false;
            Leave = false;

            DisplayPlayer("S - Service, R - Drink refill, L - Leave");

            Task.current.Succeed();
        }
        else if (Leave)
        {
            // Get a reference to the PlayerController component
            PlayerController playerController = player.GetComponent<PlayerController>();

            // Check if the reference is not null
            if (playerController != null)
            {
                // Call the TeleportToSpawnPoint() method
                playerController.TeleportToSpawnPoint();
            }
            else
            {
                Debug.LogError("PlayerController component not found on player GameObject.");
            }


            isPlayerSeated = false;
            serviceButtonPressed = false;
            leaveRestaurant = true;
            foodEaten = false;

            Stay = false;
            Leave = false;
            DisplayPlayer("S - Service button, R - Drink refill, L - Leave Restaurant");

            Task.current.Succeed();
        }
    }


    [Task]
    void DetectFoodReady()
    {
        if (foodReady)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }


    [Task]
    void Pickup()
    {
        if (Dish == null && foodReady)
        {
            // Instantiate the dish prefab
            Dish = Instantiate(dishPrefab);

            // Set the plate's parent to the bot's hand transform
            Dish.transform.SetParent(botHandTransform);

            // Set the local position and rotation of the plate
            Dish.transform.localPosition = botHandTransform.transform.localPosition;

            Dish.tag = "Dish";
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }


    [Task]
    void GiveFood()
    {
        string dishPosTag = currentTableTag == "Table4" ? "DishPos4" : "DishPos3";
        Transform dishPos = GameObject.FindGameObjectWithTag(dishPosTag)?.transform;

        if (dishPos != null && Dish != null)
        {
            // Move the dish to the dish position
            Dish.transform.SetParent(null); // Detach from the bot's hand
            Dish.transform.position = dishPos.position;
            Dish.transform.rotation = dishPos.rotation;
            foodReady = false;
            foodServed = true;
            foodOrdered = false;
            StartCoroutine(FoodEatingCountdown());
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    private IEnumerator FoodEatingCountdown()
    {

        yield return new WaitForSeconds(foodEatingTime);

        foodEaten = true;
    }

    [Task]
    void DetectRefillBtnPressed()
    {
        if (refillButtonPressed && isPlayerSeated)
        {
            Task.current.Succeed();
            refillButtonPressed = false;
        }
        else
        {
            Task.current.Fail();
        }
    }


    [Task]
    void CheckFoodServed()
    {
        if (Dish != null)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void DetectCustomerLeave()
    {
        if (leaveRestaurant)
        {
            // Get a reference to the PlayerController component
            PlayerController playerController = player.GetComponent<PlayerController>();

            // Check if the reference is not null
            if (playerController != null)
            {
                // Call the TeleportToSpawnPoint() method
                playerController.TeleportToSpawnPoint();
                Task.current.Succeed();
                isPlayerSeated = false;
                leaveRestaurant = false;
            }
            else
            {
                Task.current.Fail();
                //Debug.LogError(leaveRestaurant);
                //Debug.LogError(foodServed);
            }

        }
        else
        {
            Task.current.Fail();
        }
    }


    [Task]
    void CleanTable()
    {
        
        GameObject emptyDish = GameObject.FindGameObjectWithTag("EmptyDish");
        GameObject Dish = GameObject.FindGameObjectWithTag("Dish");

            // Check if an empty dish is found
            if (Dish != null)
            {
                // Set the empty dish's parent to the bot's hand transform
                Dish.transform.SetParent(botHandTransform);

                // Set the local position and rotation of the empty dish
                Dish.transform.localPosition = botHandTransform.transform.localPosition;

                // Indicate that the table is now clean
                Task.current.Succeed();
            }
            else if (emptyDish != null)
            {
                // Set the empty dish's parent to the bot's hand transform
                emptyDish.transform.SetParent(botHandTransform);

                // Set the local position and rotation of the empty dish
                emptyDish.transform.localPosition = botHandTransform.transform.localPosition;

                // Indicate that the table is now clean
                Task.current.Succeed();
            }
            else
            {
                // If no dishes found, indicate that the task failed
                Task.current.Fail();
            }

    }


    [Task]
    void PutDishInSink()
    {
        GameObject emptyDish = GameObject.FindGameObjectWithTag("EmptyDish");
        GameObject Dish = GameObject.FindGameObjectWithTag("Dish");

        if (Dish != null)
        {
            Destroy(Dish);
            leaveRestaurant = false;
            Task.current.Succeed();
        }
        else if(emptyDish != null)
        {
            Destroy(emptyDish);
            leaveRestaurant = false;
            Task.current.Succeed();
        }
    }

    [Task]
    void IsFireDetected()
    {
        if (kitchenFire)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void HandleKitchenFire()
    {
        extinguisherParticles.Play();
        fireParticles.Stop();
        Task.current.Succeed();
    }

    [Task]
    void EndFire()
    {
        extinguisherParticles.Stop();
        kitchenFire = false;
        Task.current.Succeed();
    }

    [Task]
    void Fire()
    {
        if (UnityEngine.Random.Range(0f, 10f) > 9.99f)
        {
            kitchenFire = true;
            fireParticles.Play();
            Task.current.Succeed();
        }
        else
        {
            //Debug.Log("Fail");
            Task.current.Fail();
        }
    }
}




