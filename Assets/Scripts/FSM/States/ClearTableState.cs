using UnityEngine;
public class ClearTableState : WaiterStateFSM
{
    public ClearTableState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("CLEAR_TABLE: Entering Clear Table State");
        Debug.Log("Clear and cleans table. Table cleaned and ready. Transitioning to Idle State.");
        // Transition to Idle State
        fsm.ChangeState(new IdleState(fsm));
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("CLEAR_TABLE: Leaving Clear Table State");
    }

}