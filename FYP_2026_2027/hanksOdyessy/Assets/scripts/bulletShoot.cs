using UnityEngine;

public class bulletShoot : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 1f;
    public float lifetime = 5f;

    private Transform player;
    private PlayerHealthManager playerHealth;

   void Start()
    {
    GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

    if (playerObject != null)
    {
        player = playerObject.transform;
        playerHealth = playerObject.GetComponentInChildren<PlayerHealthManager>();
    }

    Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.CompareTag("Player") && playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("BULLET COLLISION DETECTED!"); // please work
            Destroy(gameObject);
        }
    }
}