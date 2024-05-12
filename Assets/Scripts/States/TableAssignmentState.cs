using UnityEngine;

public class TableAssignmentState : WaiterStateFSM
{
    private bool waitingForInput = true;
    private int numberOfPeople;

    public TableAssignmentState(WaiterFSM fsm) : base(fsm) { }

    public override void Enter()
    {
        Debug.Log("TABLE_ASSIGNMENT: Entering Table Assignment State");
        Debug.Log("TABLE_ASSIGNMENT: Approaching customers and inquiring about the number of people in their party.");
        Debug.Log("INPUT A NUMBER FROM 1 - 4 (NUMBER OF CUSTOMERS)");
    }

    public override void Execute()
    {
        // Check if waiting for input
        if (waitingForInput)
        {
            // Check if any number key (1-4) is pressed
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                numberOfPeople = 1;
                waitingForInput = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                numberOfPeople = 2;
                waitingForInput = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                numberOfPeople = 3;
                waitingForInput = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                numberOfPeople = 4;
                waitingForInput = false;
            }
        }
        else
        {
            // Proceed with assigning table and guiding customers...    
            int tableNumber = Random.Range(1, 30);
            if(numberOfPeople == 1)
            {
                Debug.Log($"TABLE_ASSIGNMENT: Assigning table {tableNumber} for {numberOfPeople} customer and guiding them to the table.");
            }
            else
            {
                Debug.Log($"TABLE_ASSIGNMENT: Assigning table {tableNumber} for {numberOfPeople} customers and guiding them to the table.");
            }
            
            // Serve more customers, if any more (probability)
            if (Random.value <= 0.5f && CheckForCustomerAtWaitingArea())
            {
                Debug.Log("TABLE_ASSIGNMENT: There are more customers waiting.");
                fsm.ChangeState(new TableAssignmentState(fsm)); //transition back to Table Assignment State
                return;
            }

            // No more customers waiting, transition back to Idle
            Debug.Log("TABLE_ASSIGNMENT: No more customers waiting. Returning to Idle State.");
            fsm.ChangeState(new IdleState(fsm));
        }
    }

    public override void Exit()
    {
        Debug.Log("Leaving Table Assignment State");
    }

    private bool CheckForCustomerAtWaitingArea()
    {
        // Simulate a random chance for a customer to be present
        float randomChance = Random.Range(0f, 1f);
        return randomChance <= 0.5f; // 50% chance of customer presence
    }
}
