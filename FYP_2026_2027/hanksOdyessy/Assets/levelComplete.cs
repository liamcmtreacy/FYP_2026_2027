using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    // the LEVEL COMPLETE text on the screen
    public GameObject levelCompleteText;


    void Start()
    {
        // hides the text when the game starts
        levelCompleteText.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        // checks if Hank has touched the end platform
        if (other.CompareTag("Player"))
        {
            // shows LEVEL COMPLETE YOU ARE AWESOME type text SSon the screen
            levelCompleteText.SetActive(true);

            // stops the player from moving
            Time.timeScale = 0f;
        }
    }
}