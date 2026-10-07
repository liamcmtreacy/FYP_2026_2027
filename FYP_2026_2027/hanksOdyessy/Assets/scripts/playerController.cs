using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // how fast Hank moves
    public float speed = 5f;

    // how high Hank can jump
    public float jumpHeight = 2f;

    // gravity pulling Hank down
    public float gravity = -9.81f;

    // how sensitive the mouse is
    public float mouseSensitivity = 2f;

    // where Hank goes if he touches the floor
    public Transform spawnPoint;

    // keeps track of Hank falling
    private float verticalVelocity;

    // gets the Character Controller
    private CharacterController controller;

    // keeps track of looking up and down
    private float cameraRotation = 0f;


    void Start()
    {
        // gets the Character Controller attached to Hank
        controller = GetComponent<CharacterController>();

        // makes sure the Character Controller is turned on
        controller.enabled = true;

        // locks the mouse to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        //MOVEMENT FOR HANK

        // makes sure the Character Controller is active
        if (!controller.enabled)
        {
            controller.enabled = true;
        }

        // gets WASD movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // moves Hank forwards, backwards and sideways
        Vector3 movement = transform.right * x + transform.forward * z;


        //JUMPING

        // checks if Hank is standing on something
        if (controller.isGrounded)
        {
            // keeps Hank on the ground
            if (verticalVelocity < 0)
            {
                verticalVelocity = -2f;
            }

            // Space makes Hank jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        // gravity (wow)
        verticalVelocity += gravity * Time.deltaTime;


        //APPLY MOVEMENT

        Vector3 finalMovement = movement * speed;

        // adds jumping/falling to the movement
        finalMovement.y = verticalVelocity;

        // moves Hank
        controller.Move(finalMovement * Time.deltaTime);


        //MOUSE FOLLOWING/TRACKING

        // looks left and right
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        // looks up and down
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraRotation -= mouseY;

        // stops Hank from looking completely upside down
        cameraRotation = Mathf.Clamp(cameraRotation, -80f, 80f);

        transform.localRotation =
            Quaternion.Euler(
                cameraRotation,
                transform.localEulerAngles.y,
                0f
            );
    }


    //SPAWN RETURN

    // checks when Hank's Character Controller hits something
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // looks for an Enemy script on whatever Hank hit
        Enemy enemy = hit.collider.GetComponentInParent<Enemy>();

        // checks if Hank actually hit an enemy
        if (enemy != null)
        {
            // checks if Hank is falling
            if (verticalVelocity < 0)
            {
                // Hank jumped on the enemy
                enemy.Defeat();

                // makes Hank bounce after landing on the enemy
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
    }
}
