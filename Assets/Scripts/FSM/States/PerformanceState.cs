using UnityEngine;
public class PerformanceState : WaiterStateFSM
{
    public PerformanceState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("PERFORMANCE: Entering Performance State.");
        Debug.Log("PERFORMANCE: Plays birthday song. Display birthday visuals on screen. Do a small dance.");
        Debug.Log("PERFORMANCE: Performance over. Transitioning to Idle State.");
        // Transition to Idle State
        fsm.ChangeState(new IdleState(fsm));
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("PERFORMANCE: Leaving Performance State.");
    }
}