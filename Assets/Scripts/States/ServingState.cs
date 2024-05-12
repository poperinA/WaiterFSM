using UnityEngine;
public class ServingState : WaiterStateFSM
{
    public ServingState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("Entering Serving State");
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        
    }

    //implementation
}