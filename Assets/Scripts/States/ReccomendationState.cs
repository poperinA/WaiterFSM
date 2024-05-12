using UnityEngine;
public class RecommendationState : WaiterStateFSM
{
    public RecommendationState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("Entering Recommendation State");
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        
    }

    //implementation
}