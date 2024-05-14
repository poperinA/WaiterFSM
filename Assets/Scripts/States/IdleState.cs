using UnityEngine;

public class IdleState : WaiterStateFSM
{
    public IdleState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("IDLE: Entering Idle State.");
        Debug.Log("IDLE: Standing at post.");
    }

    public override void Execute()
    {
 
        // Check for triggers

        if (CheckForCustomerAtWaitingArea())
        {
            Debug.Log("IDLE: Customers are at waiting area. Transitioning to Table Assignment State");
            // Transition to Table Assignment State
            fsm.ChangeState(new TableAssignmentState(fsm));
        }
        else if (CheckForServiceButtonPress())
        {
            Debug.Log("IDLE: Service button pressed. Transitioning to Customer Service State");
            // Transition to Customer Service State
            fsm.ChangeState(new CustomerServiceState(fsm));
        }

        else if (CheckForCustomerLeave())
        {
            Debug.Log("IDLE: Customer leaves restaurant. Transitioning to Clear Table State");
            // Transition to Clear Table State
            fsm.ChangeState(new ClearTableState(fsm));
        }
        else if (CheckForFoodReady())
        {
            Debug.Log("IDLE: Food ready. Transitioning to Serving State");
            // Transition to Serving State
            fsm.ChangeState(new ServingState(fsm));
        }
        else
        {
            //loops back to the first if statement since it updates every frame
        }
    }

    public override void Exit()
    {
        Debug.Log("IDLE: Leaving Idle State.");
    }

    //checks
    private bool CheckForCustomerAtWaitingArea()
    {
        //simulate a random chance for a customer to be present
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.3f;
    }

    private bool CheckForServiceButtonPress()
    {
        //simulate a random chance for a customer to order
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.3f;
    }

    private bool CheckForCustomerLeave()
    {
        //simulate a random chance for a customer to leave
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.3f;
    }

    private bool CheckForFoodReady()
    {
        //simulate a random chance for a customer's food to be ready
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.3f;
    }
}
