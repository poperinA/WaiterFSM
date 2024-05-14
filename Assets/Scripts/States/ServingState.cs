using UnityEngine;
public class ServingState : WaiterStateFSM
{
    public ServingState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("SERVING: Entering Serving State");
        Debug.Log("Takes food from kitchen and serves to table.");

        if (CheckForBirthdayOrder())
        {
            Debug.Log("SERVING: All food served. Customers has a birthday order. Transitioning to Performance State.");
            // Transition to Performance State
            fsm.ChangeState(new PerformanceState(fsm));
        }
        else
        {
            Debug.Log("SERVING: All food served. Transitioning to Idle State.");
            // Transition to Idle State
            fsm.ChangeState(new IdleState(fsm));
        }
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("SERVING: Leaving Serving State");
    }

    private bool CheckForBirthdayOrder()
    {
        // Simulate a 40% chance for a birthday order
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.4f;
    }
}