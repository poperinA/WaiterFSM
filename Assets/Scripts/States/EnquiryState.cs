using UnityEngine;
public class EnquiryState : WaiterStateFSM
{
    public EnquiryState(WaiterStateFSM stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Enquiry State");
    }

    //implementation
}