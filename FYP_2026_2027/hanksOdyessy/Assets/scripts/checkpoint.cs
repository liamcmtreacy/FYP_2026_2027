using UnityEngine;
public class Checkpoint : MonoBehaviour
{
    public Transform checkpointSpawn;
    private bool activated = false;
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController player = other.GetComponentInParent<PlayerController>(); // get info from player controller

        if (player != null && !activated && checkpointSpawn != null)
        {
            // Updates Hank's spawn point
            player.spawnPoint = checkpointSpawn;
            activated = true;
            Debug.Log("Checkpoint activated!");
        }
    }
}