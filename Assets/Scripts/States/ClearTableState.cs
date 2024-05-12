using UnityEngine;
public class ClearTableState : WaiterStateFSM
{
    public ClearTableState(WaiterStateFSM stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Clear Table State");
    }

    //implementation
}