using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public int damage = 1;
    public float lifetime = 5f;

    void Start()
    {
        // Removes the bullet after 5 seconds
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Checks for a normal robot enemy
        Enemy enemy = collision.collider.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeSwordDamage(damage);
        }

        // Checks for a shooting robot enemy
        shootTargetsAtPlayer shooter = collision.collider.GetComponentInParent<shootTargetsAtPlayer>();

        if (shooter != null)
        {
            shooter.TakeSwordDamage(damage);
        }

        // Removes the bullet when it hits something
        Destroy(gameObject);
    }
}