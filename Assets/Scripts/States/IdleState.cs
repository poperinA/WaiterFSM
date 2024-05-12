using UnityEngine;

public class IdleState : WaiterStateFSM
{
    public IdleState(WaiterStateFSM stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }

    //implementation
}
