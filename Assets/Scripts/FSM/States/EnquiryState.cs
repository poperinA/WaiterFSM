using UnityEngine;
public class EnquiryState : WaiterStateFSM
{
    public EnquiryState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("ENQUIRY: Entering Enquiry State.");
        Debug.Log("ENQUIRY: Waiting for question.");
        if (CheckForRecommendation())
        {
            Debug.Log("ENQUIRY: Customer asks for recommendation. Transitioning to Recommendation State.");
            // Transition to Recommendation State
            fsm.ChangeState(new RecommendationState(fsm));
        }
        else
        {
            Debug.Log("ENQUIRY: Customer has a complaint. Transitioning to Apology State.");
            // Transition to Apology State
            fsm.ChangeState(new ApologyState(fsm));

        }

    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("ENQUIRY: Leaving Enquiry State.");
    }

    private bool CheckForRecommendation()
    {
        // Simulate a 30% chance for a customer to enquire
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.3f; // Customer is asking
    }
}