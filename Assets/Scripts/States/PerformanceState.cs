using UnityEngine;
public class PerformanceState : WaiterStateFSM
{
    public PerformanceState(WaiterStateFSM stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Performance State");
    }

    //implementation
}