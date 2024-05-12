using UnityEngine;
public class CustomerServiceState : WaiterStateFSM
{
    public CustomerServiceState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("Entering Customer Service State");
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        
    }

    //implementation
}