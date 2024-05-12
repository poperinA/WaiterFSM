using UnityEngine;
public class RecommendationState : WaiterStateFSM
{
    public RecommendationState(WaiterStateFSM stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Entering Recommendation State");
    }

    //implementation
}