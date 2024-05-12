using UnityEngine;
public class ApologyState : WaiterStateFSM
{
    public ApologyState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("Entering Apology State");
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        
    }

    //implementation
}