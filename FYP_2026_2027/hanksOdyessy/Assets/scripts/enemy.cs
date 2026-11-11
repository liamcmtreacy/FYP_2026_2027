using UnityEngine; // got a full going over
public class Enemy : MonoBehaviour
{
    public int points = 100; // how many points Hank gets
    public int health = 2; // how many hits to defeat

    public AudioClip metalSmash; // metal crunch sound

    private bool defeated = false; // stops the enemy being defeated more than once

    public Transform player; // the player Hank

    public float speed = 3f; // how fast the enemy moves
    public float stoppingDistance = 1.5f; // how close the enemy needs to be to Hank
    public float rotationSpeed = 360f; // how quickly the enemy turns towards Hank

    void Start()
    {
        // automatically finds Hank if no player has been assigned
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    
    void Update() // this got a big update // its kinda more advanced, or at least looks more?
    {   // with actual animations and not placeholders prob be more impressive
    // stops the enemy moving if it has been defeated or Hank cannot be found
    if (defeated || player == null) return;

    // calculates the direction towards Hank
    Vector3 direction = player.position - transform.position;
    direction.y = 0f; // keeps the enemy facing Hank on the ground

    float distance = direction.magnitude;

    // makes sure the enemy has a direction to face
    if (direction.sqrMagnitude > 0.001f)
    {
        direction.Normalize();

        // smoothly turns the enemy towards Hank and stops when facing him
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    // chases Hank until the enemy reaches its stopping distance
    if (distance > stoppingDistance)
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    }   


    // takes damage from the sword
    public void TakeSwordDamage(int damage)
    {
        // ignores damage if the enemy is defeated or the damage is invalid
        if (defeated || damage <= 0) return;

        health -= damage; // removes health from the enemy

        // defeats enemy when health reaches zero
        if (health <= 0)
        {
            Defeat();
        }
    }

    public void Defeat()
    {
        // prevents the enemy from being defeated more than once
        if (defeated) return;

        defeated = true; // marks the enemy as defeated

        // plays the metal crunch sound if one has been assigned
        if (metalSmash != null)
        {
            AudioSource.PlayClipAtPoint(metalSmash, transform.position);
        }

        ScoreManager.score += points; // adds points to Hank's score

        Destroy(gameObject); // removes the enemy from the scene
    }
    }
