using UnityEngine;
public class shootTargetsAtPlayer : MonoBehaviour
{
    // Hank 
    public Transform player;
    public GameObject projectilePrefab;
    // Where the missile comes from
    public Transform firePoint;
    // How far away the enemy can detect Hank (will edit a lot probably)
    public float detectionRange = 15f;

    public float fireRate = 2f;  // How long between each shot
    private float nextFireTime = 0f;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        // Checks how far away Hank is
        float distance = Vector3.Distance(transform.position, player.position);

        // Checks if Hank is within shooting range
        if (distance <= detectionRange)
        {
            // Turns towards Hank
            Vector3 direction = player.position - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            // Checks if the enemy can shoot again
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    // Shoots a missile towards Hank
    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;
        Vector3 direction = (player.position - firePoint.position).normalized;
        Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
        Debug.Log("Zapper fired a missile!");
    }
}
