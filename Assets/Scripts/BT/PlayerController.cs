using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MonoBehaviour playerMovementScript; // Reference to the player movement script

    public Transform spawnPoint; // Reference to the spawn point transform

    void Update()
    {
        // Check if the player is seated and disable the movement script if true
        if (WaiterTasks.isPlayerSeated)
        {
            if (playerMovementScript != null && playerMovementScript.enabled)
            {
                playerMovementScript.enabled = false;
            }
        }
        else
        {
            if (playerMovementScript != null && !playerMovementScript.enabled)
            {
                playerMovementScript.enabled = true;
            }
        }
    }

    public void TeleportToSpawnPoint()
    {
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            playerMovementScript.enabled = true;
        }
        else
        {
            Debug.LogError("Spawn point not assigned.");
        }
    }
}
