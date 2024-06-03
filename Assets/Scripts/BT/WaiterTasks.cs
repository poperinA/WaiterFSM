using UnityEngine;
using TMPro;
using UnityEngine.AI;
using Panda;
using System.Collections;

public class WaiterTasks : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI displayTextPlayer;
    public GameObject player;
    public MonoBehaviour playerMovementScript;
    public static bool isPlayerSeated = false;
    public static bool serviceButtonPressed = false;
    public static bool takeOrder = false;
    public static bool customerComplaint = false;
    public float foodPreparationTime = 10f;
    public ParticleSystem cookingParticles;

    public GameObject dishPrefab;
    public Transform botHandTransform;

    private NavMeshAgent navAgent;
    private Transform target;
    private GameObject customer;
    private Transform seat;
    private string currentTableTag;
    private int partySize = 0;
    private bool Stay = false;
    private bool Leave = false;
    private bool foodReady = false;
    private GameObject Dish;


    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        cookingParticles.Stop();
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
    bool DisplayPlayer(string text)
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
                Debug.Log("Guiding to: " + target.name);
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

            // Update the display text with options
            DisplayPlayer("1 - Service button, 2 - Drink refill");

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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            takeOrder = true;
            customerComplaint = false;
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
            DisplayPlayer("1 - Service button, 2 - Drink refill");
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
            DisplayPlayer("1 - Service button, 2 - Drink refill");
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
            Stay = false;
            Leave = false;
            DisplayPlayer("1 - Service button, 2 - Drink refill");
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
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }


    

}


