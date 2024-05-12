using UnityEngine;
public class OrderTakingState : WaiterStateFSM
{
    public OrderTakingState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("Entering Order Taking State");
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        
    }

    //implementation
}