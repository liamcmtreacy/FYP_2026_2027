using UnityEngine; // duh

public class trashcanlidPickUp : MonoBehaviour
{
    public GameObject trashCanLid; // made these both game objects so I can see them in the inspector
    public GameObject trashCanLid_equipped;

    void Start()
    {
        // hides the lid hank is holding when game begins
        trashCanLid_equipped.SetActive(false);
    }

    void Update()
    {
    }// left blankS

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            // originally had GetKey, but then you had to hold it
            // will check with Muhammed which he prefers
            {
                this.gameObject.SetActive(false);
                trashCanLid_equipped.SetActive(false);
            }
        }
    }

    // learned Unity names are case sensitive, caused some errors
}
