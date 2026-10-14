// reusing a lot of this code from enemy.cs
using UnityEngine;

public class IdleEnemy : MonoBehaviour
{
    // how many points Hank gets, lower cus this enemy is easier
    public int points = 40;

    // metal crunch sound
    public AudioClip metalSmash;

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

        // play metal crunch sound
        AudioSource.PlayClipAtPoint(metalSmash, transform.position);

        // add points to Hank's score
        ScoreManager.score += points;

        // destroy the enemy
        Destroy(gameObject);
    }
}
