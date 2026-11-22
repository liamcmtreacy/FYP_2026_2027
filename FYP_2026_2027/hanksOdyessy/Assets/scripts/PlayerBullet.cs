
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int damage = 1; // how much the bullet hurts, probably not necessary right now as every enemy dies instantly
    public float lifetime = 5f;

    void Start()
    {
        // Removes the bullet if it doesn't hit anything
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Checks whether the bullet hit an enemy (Can you aim?)
        Enemy enemy = collision.collider.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            // Damages the enemy using the existing enemy system
            enemy.TakeSwordDamage(damage);

            Debug.Log("Bullet hit an enemy!");
        }

        // Destroys the bullet after hitting something
        Destroy(gameObject);
    }
}

