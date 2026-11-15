using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Starts the game when the button is pressed
    public void StartGame()
    {
        SceneManager.LoadScene("level01"); // insert whichever game scene you want to switch too
    }

    public void OpenControls()
    {
        SceneManager.LoadScene("controls");
    }

    public void ReturnToMenu()
    {
    SceneManager.LoadScene("startMenu");
    }
}