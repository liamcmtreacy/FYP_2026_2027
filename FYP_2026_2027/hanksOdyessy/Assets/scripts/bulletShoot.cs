using UnityEngine;
public class bulletShoot : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 1f;
    public float lifetime = 5f;
    public float turnSpeed = 8f;

    private Transform player;
    private PlayerHealthManager playerHealth;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;

            // Find PlayerHealthManager anywhere in Hank's hierarchy
            playerHealth = playerObject.GetComponentInParent<PlayerHealthManager>();

            if (playerHealth == null)
            {
                playerHealth = playerObject.transform.root.GetComponentInChildren<PlayerHealthManager>(true);
            }

            Debug.Log("Player found by projectile!");
        }

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (player == null) return;

        // Aim towards Hank's body
        Vector3 targetPosition = player.position + Vector3.up * 1f;
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Smoothly rotate towards Hank
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            turnSpeed * Time.deltaTime
        );

        // Move forwards
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
{
    // Check if the bullet collided with Hank
    if (other.CompareTag("Player"))
    {
        Debug.Log("BULLET HIT HANK!");

        // Find PlayerHealthManager on Hank
        PlayerHealthManager healthManager = other.GetComponent<PlayerHealthManager>();

        if (healthManager != null)
        {
            healthManager.TakeDamage(damage);
            Debug.Log("Damage applied: " + damage);
        }
        else
        {
            Debug.LogError("PlayerHealthManager NOT FOUND!");
        }

        // Destroy bullet after hitting Hank
        Destroy(gameObject);
    }
}
}