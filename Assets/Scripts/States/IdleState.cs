using UnityEngine;

public class IdleState : WaiterStateFSM
{
    public IdleState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("IDLE: Standing at post");
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
        else if (CheckForOrderButtonPress())
        {
            Debug.Log("IDLE: Transitioning to Order Taking State");
            // Transition to Order Taking State
            fsm.ChangeState(new OrderTakingState(fsm));
        }
        else if (CheckForRefillButtonPress())
        {
            Debug.Log("IDLE: Transitioning to Refill State");
            // Transition to Refill State
            fsm.ChangeState(new RefillState(fsm));
        }
        else if (CheckForCustomerLeave())
        {
            Debug.Log("IDLE: Transitioning to Customer Leave State");
            // Transition to Clear Table State
            fsm.ChangeState(new ClearTableState(fsm));
        }
        else if (CheckForFoodReady())
        {
            Debug.Log("IDLE: Transitioning to Food Ready State");
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
        //Debug.Log("Leaving Idle State");
    }

    //checks
    private bool CheckForCustomerAtWaitingArea()
    {
        //simulate a random chance for a customer to be present
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.8f; // Customer is present
    }

    private bool CheckForOrderButtonPress()
    {
        //simulate a random chance for a customer to order
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.5f; // Customer wants to order
    }

    private bool CheckForRefillButtonPress()
    {
        //simulate a random chance for a customer to ask for refill
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.3f; // Customer requests refill
    }

    private bool CheckForCustomerLeave()
    {
        //simulate a random chance for a customer to leave
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.3f; // Customer leaves
    }

    private bool CheckForFoodReady()
    {
        //simulate a random chance for a customer's food to be ready
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.3f; // Customer's food is ready
    }
}
