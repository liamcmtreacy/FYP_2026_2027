using UnityEngine;

public class PlayerShootsGun : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab; // obj

    public float bulletSpeed = 20f; // I'll play around with this
    public float fireRate = 0.3f;
    private float nextFireTime;

    void Update()
    {// LEFT MOUSE
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
        Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || shootingPoint == null)
        {
            Debug.Log("Bullet prefab or shooting point is missing!");
            return;
        }//

        nextFireTime = Time.time + fireRate;

        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = shootingPoint.forward * bulletSpeed;
        }

        Debug.Log("Hank shot his gun");
    }
}
