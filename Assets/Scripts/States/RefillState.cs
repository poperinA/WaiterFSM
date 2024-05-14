using UnityEngine;
public class RefillState : WaiterStateFSM
{
    public RefillState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("REFILL: Entering Refill State.");
        Debug.Log("REFILL: Takes cup to refill. Refill drink in the kitchen. Bring back refilled drink.");
        Debug.Log("REFILL: Refill done. 'Anything else?' Transitioning to Customer State.");
        // Transition to Customer Service State
        fsm.ChangeState(new CustomerServiceState(fsm));
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("REFILL: Leaving Refill State.");
    }

}