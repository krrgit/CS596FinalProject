using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingZombie : MonoBehaviour
{
    public bool test = false;
    public bool togglecheck = false;
    public float speed = 2f; // digSpeed
    public float yground = 2.7f;

    private Rigidbody rb;
    private GameObject player; // Reference to the player object

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player object
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (test)
        {
            Dig();
        }

        if (togglecheck)
        {
            transform.Translate(Vector3.up * (speed * 3f) * Time.deltaTime);
            if (transform.position.y >= yground)
            {
                rb.useGravity = true;
                togglecheck = false;
            }
        }

        if (rb.useGravity)
        {
            KeepWalking();
        }
    }

    void Dig()
    {
        if (player != null)
        {
            // Get the player's position
            Vector3 playerPosition = player.transform.position;

            // Calculate the distance between the player and the flying zombie
            float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

            // If Enemy is close enough, go up
            if (distanceToPlayer < 5f)
            {
                togglecheck = true;
                test = false;
                return;
            }

            // Ignore the Y-component of the player's position
            playerPosition.y = transform.position.y;

            // Calculate the direction from the current position to the modified player position
            Vector3 direction = playerPosition - transform.position;
            direction.Normalize(); // Normalize to get a unit vector

            // Move towards the modified player position
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    void KeepWalking()
    {
        // Get the player's position
        Vector3 playerPosition = player.transform.position;

        // Calculate the distance between the player and the flying zombie
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

        // Ignore the Y-component of the player's position
        playerPosition.y = transform.position.y;

        // Calculate the direction from the current position to the modified player position
        Vector3 direction = playerPosition - transform.position;
        direction.Normalize(); // Normalize to get a unit vector

        // Move towards the modified player position
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
