using UnityEngine;

public class WaiterController : MonoBehaviour
{
    public WaiterTasks waiterTasks;

    void Update()
    {
        // Detect service button press
        if (Input.GetKeyDown(KeyCode.Alpha1) && WaiterTasks.isPlayerSeated)
        {
            WaiterTasks.serviceButtonPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && WaiterTasks.isPlayerSeated)
        {
            WaiterTasks.refillButtonPressed = true;
        }
    }
}
