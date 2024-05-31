using UnityEngine;
using TMPro;
using UnityEngine.AI;
using Panda;

public class WaiterTasks : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI displayText1;
    public GameObject player;
    public MonoBehaviour playerMovementScript;
    public static bool isPlayerSeated = false;
    public static bool serviceButtonPressed = false;

    private NavMeshAgent navAgent;
    private Transform target;
    private GameObject customer;
    private int partySize = 0;
    private Transform seat;
    private bool optionSelected = false;
    private int selectedOption = 0;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
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
        if (displayText1 != null)
        {
            displayText1.text = text;
            displayText1.enabled = text != "";
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
        string tableTag = partySize <= 2 ? "Table4" : "Table3";
        string seatTag = partySize <= 2 ? "Table4Seat" : "Table3Seat";

        if (task.isStarting)
        {
            target = GameObject.FindGameObjectWithTag(tableTag)?.transform;
            seat = GameObject.FindGameObjectWithTag(seatTag)?.transform;

            if (target != null)
            {
                Debug.Log("Guiding to: " + target.name);
                navAgent.stoppingDistance = 1f;
                navAgent.SetDestination(target.position);
            }
            else
            {
                Debug.LogError("Table with tag " + tableTag + " not found.");
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
            serviceButtonPressed = false; // Reset for next interaction
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void GiveOptions()
    {
        var task = Task.current;

        DisplayPlayer("1 - Order, 2 - Complain");

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedOption = 1;
            optionSelected = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedOption = 2;
            optionSelected = true;
        }

        if (optionSelected)
        {
            if (selectedOption == 1)
            {
                // Trigger order-taking behavior
                Debug.Log("Order selected");
            }
            else if (selectedOption == 2)
            {
                // Trigger complaint-handling behavior
                Debug.Log("Complaint selected");
            }

            optionSelected = false; // Reset for next interaction
            selectedOption = 0; // Reset selected option
            task.Succeed();
        }
    }
}


