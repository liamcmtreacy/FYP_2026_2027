using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // keeps track of Hank's score
    public static int score = 0;

    // the score text shown on the screen
    public TMP_Text scoreText;


    void Start()
    {
        // starts Hank's score at zero
        score = 0;

        // shows the starting score
        scoreText.text = "Score: " + score;
    }


    void Update()
    {
        // updates the score on screen
        scoreText.text = "Score: " + score;
    }
}