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
        Debug.Log("hello");
        // Check for triggers
        if (CheckForCustomerAtWaitingArea())
        {
            Debug.Log("TA");
            // Transition to Table Assignment State
            fsm.ChangeState(new TableAssignmentState(fsm));
        }
        else if (CheckForOrderButtonPress())
        {
            Debug.Log("OT");
            // Transition to Order Taking State
            fsm.ChangeState(new OrderTakingState(fsm));
        }
        else if (CheckForRefillButtonPress())
        {
            Debug.Log("R");
            // Transition to Refill State
            fsm.ChangeState(new RefillState(fsm));
        }
        else
        {
            Debug.Log("Dawg");
        }
    }

    public override void Exit()
    {
        Debug.Log("Leaving Idle State");
    }

    private bool CheckForCustomerAtWaitingArea()
    {
        // Simulate a random chance for a customer to be present
        // Adjust the probability as needed
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.8f; // Customer is present
    }

    private bool CheckForOrderButtonPress()
    {
        // Simulate a random chance for a customer to be present
        // Adjust the probability as needed
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.5f; // Customer is present
    }

    private bool CheckForRefillButtonPress()
    {
        // Simulate a random chance for a customer to be present
        // Adjust the probability as needed
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.3f; // Customer is present
    }
}
