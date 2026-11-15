using UnityEngine; // This was a huge pain to get working

public class shootTargetsAtPlayer : MonoBehaviour
{
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
                GameObject.FindGameObjectWithTag("Player"); // player tag

            if (playerObject != null)
            {
                player = playerObject.transform;
                Debug.Log("Enemy found Hank!"); // debug for testing
            }
            else
            {
                Debug.LogError("Could not find Hank! Check Player tag."); // same as above
            }
        }
    }

    void Update()
    {
        // Check that all required references exist
        if (player == null || projectilePrefab == null || firePoint == null)
            return;

        // Check distance between enemy and Hank
        float distance = Vector3.Distance(transform.position, player.position);

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
        Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.LookRotation(direction)
        );

        Debug.Log("Zapper fired a projectile!"); // final debug
    }
}
