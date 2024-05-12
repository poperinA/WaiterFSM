using UnityEngine;

public class IdleState : WaiterStateFSM
{
    public IdleState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }

    public override void Execute()
    {
 
        // Check for triggers

        if (CheckForCustomerAtWaitingArea())
        {
            Debug.Log("Transitioning to Table Assignment State");
            // Transition to Table Assignment State
            fsm.ChangeState(new TableAssignmentState(fsm));
        }
        else if (CheckForOrderButtonPress())
        {
            Debug.Log("Transitioning to Order Taking State");
            // Transition to Order Taking State
            fsm.ChangeState(new OrderTakingState(fsm));
        }
        else if (CheckForRefillButtonPress())
        {
            Debug.Log("Transitioning to Refill State");
            // Transition to Refill State
            fsm.ChangeState(new RefillState(fsm));
        }
        else
        {
            //will continue to stay idle
            Debug.Log("No action needed; IDLE");

            //loops back to the first if statement since it updates every frame
        }
    }

    public override void Exit()
    {
        Debug.Log("Leaving Idle State");
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
}
