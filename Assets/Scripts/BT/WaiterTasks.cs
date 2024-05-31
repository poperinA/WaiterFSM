using UnityEngine;
using TMPro;
using UnityEngine.AI;
using Panda;

public class WaiterTasks : MonoBehaviour
{
    public TextMeshProUGUI displayText;

    private NavMeshAgent navAgent;
    private Transform target;
    private GameObject customer;
    private int partySize = 0;
    private Transform seat;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

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
            task.Succeed();
        }

    }
}
