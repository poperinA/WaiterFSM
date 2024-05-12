using UnityEngine;
public class RefillState : WaiterStateFSM
{
    public RefillState(WaiterStateFSM stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Refill State");
    }

    //implementation
}