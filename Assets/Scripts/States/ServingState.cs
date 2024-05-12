using UnityEngine;
public class ServingState : WaiterStateFSM
{
    public ServingState(WaiterStateFSM stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Serving State");
    }

    //implementation
}