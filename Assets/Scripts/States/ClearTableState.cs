using UnityEngine;
public class ClearTableState : WaiterStateFSM
{
    public ClearTableState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("Entering Clear Table State");
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        
    }

    //implementation
}