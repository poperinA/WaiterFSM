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
                Meal = GenerateMainMeal();
                waitingForInput = false;
                mealChosen = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Meal = GenerateMainMeal();
                waitingForInput = false;
                mealChosen = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Meal = GenerateKidsMeal();
                waitingForInput = false;
                mealChosen = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Meal = GenerateDessert();
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
                  "1: Main Meal 1\n" +
                  "2: Main Meal 2\n" +
                  "3: Kids Meal 1\n" +
                  "4: Kids Meal 2\n" +
                  "5: Birthday Meal");
    }

    private string GenerateMainMeal()
    {
        // Generate random main meal names
        string[] mainMeals = { "Chicken Alfredo", "Grilled Salmon", "Pasta Primavera", "Steak Frites", "Vegetable Stir-Fry" };
        int index = Random.Range(0, mainMeals.Length);
        return mainMeals[index];
    }

    private string GenerateKidsMeal()
    {
        // Generate random kids meal names
        string[] kidsMeals = { "Chicken Nuggets", "Cheeseburger", "Macaroni and Cheese", "Pizza", "PB&J Sandwich" };
        int index = Random.Range(0, kidsMeals.Length);
        return kidsMeals[index];
    }

    private string GenerateDessert()
    {
        // Generate random dessert names
        string[] desserts = { "Chocolate Cake", "Apple Pie", "Ice Cream Sundae", "Cheesecake", "Fruit Tart" };
        int index = Random.Range(0, desserts.Length);
        return desserts[index];
    }

    private bool CheckForEnquiry()
    {
        // Simulate a 50% chance for a customer to enquire
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.5f; // Customer is asking
    }
}
