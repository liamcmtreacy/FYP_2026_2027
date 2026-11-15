using UnityEngine;
public class GunPickup : MonoBehaviour
{
    private WeaponManager weaponManager; // needed for swapping
    private PlayerController playerInRange;

    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player != null)
        {
            playerInRange = player;
            weaponManager = player.GetComponent<WeaponManager>();

            Debug.Log("Hank is near the Gun. you can press E to pick it up"); // Debugs are very handy now
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player != null && player == playerInRange)
        {
            playerInRange = null;
            Debug.Log("Hank left the Gun, Sword remains");
        }
    }

    void Update()
    {
        if (playerInRange != null && Input.GetKeyDown(KeyCode.E))
        {
            PickUpGun();
        }
    }

    void PickUpGun()
    {
        if (weaponManager == null)
        {
            Debug.LogWarning("Weapon Manager not found on Player, weapons cannot be swapped");
            return;
        }
        weaponManager.CollectGun();
        Debug.Log("Hank picked up the Gun");
        gameObject.SetActive(false);
    }
}
