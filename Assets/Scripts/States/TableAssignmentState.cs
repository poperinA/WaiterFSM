using UnityEngine;
public class TableAssignmentState : WaiterStateFSM
{
    public TableAssignmentState(WaiterFSM fsm) : base(fsm) { }


    public override void Enter()
    {
        Debug.Log("Entering Table Assignment State");
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        
    }

    //implementation
}