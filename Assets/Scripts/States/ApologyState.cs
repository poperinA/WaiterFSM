using UnityEngine;
public class ApologyState : WaiterStateFSM
{
    public ApologyState(WaiterStateFSM stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Apology State");
    }

    //implementation
}