using UnityEngine;

public class Enemy : MonoBehaviour
{
    // how many points Hank gets
    public int points = 100;

    // metal crunch sound
    public AudioClip metalSmash;

    // stops the enemy being defeated more than once
    private bool defeated = false;

    // the player Hank
    public Transform player;// THIS IS NEEDED SO HE CAN BE FOLLWOED

    // how fast the enemy moves
    public float speed = 3f;

    // how close the enemy needs to be to Hank
    public float stoppingDistance = 1.5f;

    // Update was originally deelted, but I added it back because I want the enemy to be looking for hank every frame
     void Update()
    {
        // makes the enemy follow Hank
        if (player != null)
        {
            // checks how far away Hank is
            float distance = Vector3.Distance(transform.position, player.position);

            // moves towards Hank if he is far enough away
            if (distance > stoppingDistance)
            {
                Vector3 direction = (player.position - transform.position).normalized;

                // keeps the enemy moving on the ground
                direction.y = 0;

                transform.position += direction * speed * Time.deltaTime;

                // turns the enemy towards Hank
                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(direction);
                }
            }
        }
    }
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
