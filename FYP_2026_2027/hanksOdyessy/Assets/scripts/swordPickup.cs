using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public Transform holdingSword; // Where Hank holds the sword (will likely get changed a lot)

    public Vector3 swordPosition; // Position of sword when picked up
    public Vector3 swordRotation; // Rotation of sword when picked up

    private bool playerNearby = false; // Checks if Hank is near the sword, so can be equipped
    private bool pickedUp = false; // Checks if Hank has picked up the sword (SIMPLE T/F)

    void Start()
    {
    // still empty
    }

    void Update()
    {
        // Checks if Hank presses E near the sword
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && !pickedUp)
        {
            PickUpSword();
        }
    }

    // Picksup
    void PickUpSword()
    {
        pickedUp = true;

        transform.SetParent(holdingSword);
        
        transform.localPosition = swordPosition;
        transform.localRotation = Quaternion.Euler(swordRotation);

        Debug.Log("SWORD PICKED UP!"); //console log for test (errors)

        GetComponent<Collider>().enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    // Checks when Hank leaves the pickup area
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
