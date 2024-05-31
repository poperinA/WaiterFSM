using UnityEngine;

public class WaiterController : MonoBehaviour
{
    public WaiterTasks waiterTasks;
    public GameObject player;

    void Update()
    {
        // Detect service button press (KeyCode.Alpha1 for demonstration)
        if (Input.GetKeyDown(KeyCode.Alpha1) && WaiterTasks.isPlayerSeated)
        {
            WaiterTasks.serviceButtonPressed = true;
        }

    }
}
