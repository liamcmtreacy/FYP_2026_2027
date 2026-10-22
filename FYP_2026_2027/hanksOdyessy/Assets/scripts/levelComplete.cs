
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    // the LEVEL COMPLETE text on the screen
    public GameObject levelCompleteText;

    // the level complete sound
    public AudioClip woo;

    // checks if the level has been completed
    private bool levelCompleted = false;


    void Start()
    {
        // hides the text when the game starts
        levelCompleteText.SetActive(false);

        // makes sure the game is not paused
        Time.timeScale = 1f;
    }


    void Update()
    {
        // press R to retry after completing the level
        if (levelCompleted && Input.GetKeyDown(KeyCode.R))
        {
            // restarts the current level
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        // checks if Hank has touched the end platform
        if (other.CompareTag("Player") && !levelCompleted)
        {
            // marks the level as completed
            levelCompleted = true;

            // shows the level complete text
            levelCompleteText.SetActive(true);

            // plays the sound
            AudioSource.PlayClipAtPoint(woo, transform.position);

            // stops the player from moving
            Time.timeScale = 0f;
        }
    }
}
