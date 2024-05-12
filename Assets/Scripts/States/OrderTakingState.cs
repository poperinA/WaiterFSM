using UnityEngine;
public class OrderTakingState : WaiterStateFSM
{
    public OrderTakingState(WaiterStateFSM stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Order Taking State");
    }

    //implementation
}