
using UnityEngine;                      
///https://www.youtube.com/watch?v=R2ahXdQ9krQ&list=LL&index=1 huge help
using System.Collections.Generic;

public class swingAttackSword : MonoBehaviour
{
    // how fast the sword swings
    public float swingDuration = 0.31f; // fekt good

    // how far the sword rotates when attacking
    public float swingAngle = 100f;

    // the actual sword model
    public Transform swordWeapon;

    // hit radius. I changed this
    public float hitRadius = 10f; // made hitbox bigger to be more playable

    private bool isAttacking = false;
    private bool returning = false;

    private float swingTimer = 0f;

    private Quaternion startingRotation;
    private Quaternion attackRotation;

    // prevents hitting the same enemy twice per swing
    private List<Enemy> hitEnemies = new List<Enemy>();

    void Start()
    {
        startingRotation = swordWeapon.localRotation; // so now it remembers sword position
        attackRotation = startingRotation * Quaternion.Euler(swingAngle, 0f, 0f); // does math for rotation
    }

    void Update()
    {
        // left mouse button attacks
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            returning = false;
            swingTimer = 0f;
            hitEnemies.Clear();
        }

        // controls the sword swing
        if (isAttacking)
        {
            swingTimer += Time.deltaTime;

            // swings the sword forward
            if (!returning)
            {
                swordWeapon.localRotation = Quaternion.Slerp(startingRotation, attackRotation, swingTimer / (swingDuration / 2f)); // Updated

                // checks for enemies during the swing
                CheckForEnemies();

                if (swingTimer >= swingDuration / 2f)
                {
                    returning = true;
                    swingTimer = 0f;
                }
            }
            // returns the sword to its resting position
            else
            {
                swordWeapon.localRotation = Quaternion.Slerp(attackRotation, startingRotation, swingTimer / (swingDuration / 2f));

                if (swingTimer >= swingDuration / 2f)
                {
                    swordWeapon.localRotation = startingRotation;
                    isAttacking = false;
                }
            }
        }
    }

    // checks if the sword hits an enemy
    void CheckForEnemies()
    {
        Collider[] hits = Physics.OverlapSphere(swordWeapon.position, hitRadius);

        foreach (Collider hit in hits)
        {
            Enemy enemy = hit.GetComponentInParent<Enemy>();

            if (enemy != null && !hitEnemies.Contains(enemy))
            {
                enemy.TakeSwordDamage(1);
                hitEnemies.Add(enemy);

                Debug.Log("Sword hit enemy!");
            }
        }
    }
}
