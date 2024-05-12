using UnityEngine;
public class RefillState : WaiterStateFSM
{
    public RefillState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("Entering Refill State");
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        
    }

    //implementation
}