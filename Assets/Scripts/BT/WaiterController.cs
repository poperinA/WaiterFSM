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
        else if (Input.GetKeyDown(KeyCode.Alpha2) && WaiterTasks.isPlayerSeated && WaiterTasks.foodServed && !WaiterTasks.inService)
        {
            WaiterTasks.refillButtonPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && WaiterTasks.isPlayerSeated && WaiterTasks.dishCleared)
        {
            WaiterTasks.leaveRestaurant = true;
        }

        if (WaiterTasks.foodEaten)
        {
            // Store the position of the current dish

            var Dish = GameObject.FindGameObjectWithTag("Dish");

            Debug.Log(Dish);

            Vector3 dishPosition = Dish.transform.position;

            // Destroy the current dish
            Destroy(Dish);

            // Instantiate the empty dish prefab at the same position
            GameObject emptyDish = Instantiate(waiterTasks.emptyDishPrefab, dishPosition, Quaternion.identity);

            // Add the "EmptyDish" tag to the instantiated empty dish
            emptyDish.tag = "EmptyDish";

            WaiterTasks.foodEaten = false;
            WaiterTasks.dishCleared = true;
        }

    }
}
