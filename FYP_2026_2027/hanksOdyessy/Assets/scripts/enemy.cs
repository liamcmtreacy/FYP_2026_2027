using UnityEngine;

public class Enemy : MonoBehaviour
{
    // how many points Hank gets
    public int points = 100;

    // stops the enemy being defeated more than once
    private bool defeated = false;


    public void Defeat()
    {
        // don't give points more than once
        if (defeated)
        {
            return;
        }

        // enemy has been defeated
        defeated = true;

        // add points to Hank's score
        ScoreManager.score += points;

        // destroy the enemy
        Destroy(gameObject);
    }
}