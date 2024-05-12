using UnityEngine;
public class EnquiryState : WaiterStateFSM
{
    public EnquiryState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("Entering Enquiry State");
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        
    }

    //implementation
}