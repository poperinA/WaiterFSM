using UnityEngine;
public class CustomerServiceState : WaiterStateFSM
{
    public CustomerServiceState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("CUSTOMER_SERVICE: Entering Customer Service State.");
        Debug.Log("'Hi! How can I help you?'");

        if (CheckForOrder())
        {
            Debug.Log("CUSTOMER_SERVICE: Customers want to order. Transitioning to Order Taking State.");
            // Transition to Order Taking State
            fsm.ChangeState(new OrderTakingState(fsm));
        }
        else if (CheckForComplaint())
        {
            Debug.Log("CUSTOMER_SERVICE: Customers have a complaint. Transitioning to Apology State.");
            // Transition to Apology State
            fsm.ChangeState(new ApologyState(fsm));
        }
        else if (CheckForRefill())
        {
            Debug.Log("CUSTOMER_SERVICE: Customers wants a drink refill. Transitioning to Refill State.");
            // Transition to Refill State
            fsm.ChangeState(new RefillState(fsm));
        }
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        
    }

    private bool CheckForOrder()
    {
        // Simulate a 50% chance for a customer to order
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.5f;
    }

    private bool CheckForComplaint()
    {
        // Simulate a 30% chance for a customer to complain
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.3f;
    }

    private bool CheckForRefill()
    {
        // Simulate a 30% chance for a customer to want a refill
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.3f;
    }
}