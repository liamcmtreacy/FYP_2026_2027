
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

    // This stores the camera that follows behind Hank.
    public Transform playerCamera;


    void Start()
    {
        // gets the Character Controller attached to Hank
        controller = GetComponent<CharacterController>();

        // makes sure the Character Controller is turned on
        controller.enabled = true;

        //Puts Hank at the spawn point when the game starts
        if (spawnPoint != null)
        {
        controller.enabled = false;
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        controller.enabled = true;
        }

        // locks the mouse to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Finds Main Camera underneath Player if it hasn't been// assigned in the Inspector.
        if (playerCamera == null)
        {
            Camera childCamera = GetComponentInChildren<Camera>();

            if (childCamera != null)
            {
                playerCamera = childCamera.transform;
            }
        }

        // starts the camera with its current up/down angle (feels kinda cinematic to me)
        if (playerCamera != null)
        {
            cameraRotation = playerCamera.localEulerAngles.x;

            // converts the angle to a value between -180 and 180
            if (cameraRotation > 180f)
            {
                cameraRotation -= 360f;
            }
        }
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

        // stops Hank moving faster diagonally
        movement = Vector3.ClampMagnitude(movement, 1f);


        //JUMPING

        // checks if Hank is standing on something
        if (controller.isGrounded)
        {
            // keeps Hank on the ground
            if (verticalVelocity < 0f)
            {
                verticalVelocity = -1f; // 2 caused issues so changed to 1
            }

            // Space makes Hank jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            // gravity pulls Hank down while he is in the air
            verticalVelocity += gravity * Time.deltaTime;
        }


        //APPLY MOVEMENT

        Vector3 finalMovement = movement * speed;

        // adds jumping/falling to the movement
        finalMovement.y = verticalVelocity;

        // moves Hank
        controller.Move(finalMovement * Time.deltaTime);

        // The mouse rotates Hank left and right, and rotates the
        // child camera up and down.
        // MOUSE FOLLOWING/TRACKING

        // looks left and right
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        // looks up and down
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // rotates Hank left and right
        transform.Rotate(Vector3.up * mouseX);

        // changes the up and down camera angle
        cameraRotation -= mouseY;

        // stops the camera looking completely upside down
        cameraRotation = Mathf.Clamp(cameraRotation, -80f, 80f);

        // rotates the camera up and down
        if (playerCamera != null)
        {
            playerCamera.localRotation =
                Quaternion.Euler(cameraRotation, 0f, 0f);
        }
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
            // checks if Hank is falling and hitting the top of the enemy
            if (verticalVelocity < 0f && hit.normal.y > 0.5f)
            {
                // Hank jumped on the enemy
                enemy.Defeat();

                // makes Hank bounce after landing on the enemy
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
    }

    // RESPAWN HANK (caused many problems)
    public void Respawn()
    {
        // checks that the spawn point exists
        if (spawnPoint == null)
        {
            Debug.LogWarning("SpawnPoint has not been assigned!");
            return;
        }

        // stops the Character Controller while moving Hank
        controller.enabled = false;

        // moves Hank back to the spawn point
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        verticalVelocity = 0f;

        // turns the controller back on
        controller.enabled = true;
    }
}
