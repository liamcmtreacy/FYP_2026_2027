// help from old code on placement
using UnityEngine;
using UnityEngine.SceneManagement;

public class floorManager : MonoBehaviour
{
    // checks if Hank touches the ground
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hank dies (rip)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Update()
    {
        // press R to retry
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
