using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // speed of the platform
    public float speed = 1f;

    // how high the platform will move
    public float height = 3f;

    // saves where the platform starts
    private Vector3 startPosition;

    void Start()
    {
        // gets the starting position of the platform
        startPosition = transform.position;
    }

    void Update()
    {
        // makes the platform move up and down
        float movement = Mathf.PingPong(Time.time * speed, height);

        // keeps the x and z the same while changing the y position
        transform.position = new Vector3(
            startPosition.x,
            startPosition.y + movement,
            startPosition.z
        );
    }
}