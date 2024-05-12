using UnityEngine;
public class TableAssignmentState : WaiterStateFSM
{
    public TableAssignmentState(WaiterStateFSM stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Table Assignment State");
    }

    //implementation
}