using UnityEngine;
public class ApologyState : WaiterStateFSM
{
    public ApologyState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("APOLOGY: Entering Apology State.");

        Debug.Log("APOLOGY: 'I am sorry for any inconveniences caused. You are welcomed to still order.' ");

        if (CheckForApproval())
        {
            Debug.Log("APOLOGY: Customer accepts and is ready to order. Transitioning to Order Taking State.");
            // Transition to Order Taking State
            fsm.ChangeState(new OrderTakingState(fsm));
        }
        else
        {
            Debug.Log("APOLOGY: Customers are still unhappy and leaves the restaurant. Transitioning to Idle State.");
            // Transition to Idle State
            fsm.ChangeState(new IdleState(fsm));
        }
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        
    }

    private bool CheckForApproval()
    {
        // Simulate a 50% chance for a customer to enquire
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.5f; // Customer is asking
    }
}