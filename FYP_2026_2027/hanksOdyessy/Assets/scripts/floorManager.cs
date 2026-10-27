 // help from old code on placement
using UnityEngine;

public class floorManager : MonoBehaviour
//pubc class scoreManager: MonoBehaviour "will attempt to add back later"
{
    // finds Hank's movement script
    private PlayerController player;
    void Update()
    {
        // press R to retry
        if (Input.GetKeyDown(KeyCode.R))
        {
            // finds Hank's movement script
            player = FindFirstObjectByType<PlayerController>();

            // sends Hank back to the spawn point
            if (player != null)
            {
                player.Respawn();
            }
        }
    }
}
// whole script had to basically be rewritten as it broke my code
