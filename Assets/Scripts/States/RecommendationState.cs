using UnityEngine;
public class RecommendationState : WaiterStateFSM
{
    private bool waitingForInput = true;
    private bool Approve;

    private string Meal;
    public RecommendationState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("RECOMMENDATION: Entering Recommendation State.");

        Debug.Log("RECOMMENDATION: Displays top 3 top meal picks in the past two weeks.");

        DisplayMenu();
        DisplayOptions();

    }

    public override void Execute()
    {
        if (waitingForInput)
        {

            // Check if any number key (1-3) is pressed
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                waitingForInput = false;
                Approve = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                waitingForInput = false;
                Approve = false;
            }

        }
        else
        {
            if (Approve)
            {
                Debug.Log("RECOMMENDATION: Customer accepts and is ready to order. Transitioning to Order Taking State.");
                // Transition to Order Taking State
                fsm.ChangeState(new OrderTakingState(fsm));
            }
            else
            {
                Debug.Log("RECOMMENDATION: Customers is unhappy and complains. Transitioning to Apology State.");
                // Transition to Apology State
                fsm.ChangeState(new ApologyState(fsm));
            }
        }
    }

    public override void Exit()
    {
        Debug.Log("RECOMMENDATION: Leaving Recommendation State.");
    }

    private void DisplayMenu()
    {
        Debug.Log("Menu:\n" +
                  $"1: Grilled Salmon\n" +
                  $"2: Macaroni and Cheese\n" +
                  $"3: Chicken Alfredo");
    }

    private void DisplayOptions()
    {
        Debug.Log("Menu:\n" +
                  "1: Accept\n" +
                  "2: Reject");
    }

}