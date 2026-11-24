using UnityEngine;

public class shootTargetsAtPlayer : MonoBehaviour
{
    // Enemy health and points
    public int points = 100;
    public int health = 2;

    // Metal crunch sound
    public AudioClip metalSmash;

    // Stops the enemy being defeated more than once
    private bool defeated = false;

    // Hank
    public Transform player;

    // The projectile prefab
    public GameObject projectilePrefab;

    // Where the projectile comes from
    public Transform firePoint;

    // How far away the enemy can detect Hank
    public float detectionRange = 15f;

    // How long between each shot
    public float fireRate = 2f;

    private float nextFireTime = 0f;

    void Start()
    {
        // Find Hank using the Player tag
        if (player == null)
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
                Debug.Log("Enemy found Hank!");
            }
            else
            {
                Debug.LogError("Could not find Hank! Check Player tag.");
            }
        }
    }

    void Update()
    {
        // Stops the enemy shooting if defeated
        if (defeated) return;

        // Check that all required references exist
        if (player == null || projectilePrefab == null || firePoint == null)
            return;

        // Check distance between enemy and Hank
        float distance = Vector3.Distance(
            transform.position, player.position);

        // Check if Hank is within shooting range
        if (distance <= detectionRange)
        {
            // Turn towards Hank
            Vector3 direction = player.position - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            // Check if the enemy can shoot again
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    // Shoot a projectile towards Hank
    void Shoot()
    {
        Vector3 direction =(player.position - firePoint.position).normalized;
        // Create the projectile
        Instantiate(projectilePrefab,firePoint.position,Quaternion.LookRotation(direction));
        Debug.Log("Zapper fired a projectile!");
    }

    // Takes damage from Hank's sword
    public void TakeSwordDamage(int damage)
    {
        // Ignores damage if already defeated or damage is invalid
        if (defeated || damage <= 0) return;

        // Removes health from the enemy
        health -= damage;

        // Defeats the enemy when health reaches zero
        if (health <= 0)
        {
            Defeat();
        }
    }

    // Defeats the shooting enemy (taken from enemy.cs)
    public void Defeat()
    {
        // Prevents the enemy being defeated more than once
        if (defeated) return;

        defeated = true;

        // Plays the metal crunch sound
        if (metalSmash != null)
        {
            AudioSource.PlayClipAtPoint(metalSmash, transform.position);
        }

        // Adds points to Hank's score
        ScoreManager.score += points;

        // Removes the enemy from the scene
        Destroy(gameObject);
    }
}
