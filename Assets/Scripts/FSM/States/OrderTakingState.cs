using UnityEngine;

public class OrderTakingState : WaiterStateFSM
{
    private bool waitingForInput = true;
    private bool mealChosen = false;

    private string Meal;

    public OrderTakingState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("ORDER_TAKING: Entering Order Taking State.");

        if (CheckForEnquiry())
        {
            Debug.Log("ORDER_TAKING: Customers have an enquiry. Transitioning to Enquiry State.");
            // Transition to Enquiry State
            fsm.ChangeState(new EnquiryState(fsm));
        }
        else if (!mealChosen)
        {
            Debug.Log("ORDER_TAKING: Customers ready to order. 'What would you like to order?'");
            Debug.Log("INPUT A NUMBER FROM 1 - 5 (MENU)");
            DisplayMenu();
        }
    }

    public override void Execute()
    {
        if (waitingForInput)
        {

            // Check if any number key (1-5) is pressed
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Meal = "Chicken Alfredo";
                waitingForInput = false;
                mealChosen = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Meal = "Grilled Salmon";
                waitingForInput = false;
                mealChosen = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Meal = "Macaroni and Cheese";
                waitingForInput = false;
                mealChosen = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Meal = "Pizza";
                waitingForInput = false;
                mealChosen = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Meal = "Birthday Cake";
                waitingForInput = false;
                mealChosen = true;
            }
        }
        else if (mealChosen)
        {
            Debug.Log($"Customer chose {Meal}; order completed. Transitioning to Idle State");
            mealChosen = false;
            // Transition to Idle State
            fsm.ChangeState(new IdleState(fsm));
        }
    }

    public override void Exit()
    {
        Debug.Log("ORDER_TAKING: Leaving Order Taking State.");
    }

    private void DisplayMenu()
    {
        Debug.Log("Menu:\n" +
                  "1: Chicken Alfredo\n" +
                  "2: Grilled Salmon\n" +
                  "3: Macaroni and Cheese\n" +
                  "4: Pizza\n" +
                  "5: Birthday Meal");
    }

    private bool CheckForEnquiry()
    {
        // Simulate a 50% chance for a customer to enquire
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.5f; // Customer is asking
    }
}
