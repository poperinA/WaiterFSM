using UnityEngine;
public class CustomerServiceState : WaiterStateFSM
{
    public CustomerServiceState(WaiterStateFSM stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Customer Service State");
    }

    //implementation
}