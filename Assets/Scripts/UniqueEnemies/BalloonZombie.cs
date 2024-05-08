using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonZombie : MonoBehaviour
{
    public bool test = false;
    public float speed = 2f; // Speed
    public float yground = 1.10f;
    private Rigidbody rb;

    private GameObject player; // Reference to the player object

    public GameObject Balloon;
    public GameObject Strings;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player object
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (test)
        {
            FlyTowardsPlayer(); // testing
        }

        if (transform.position.y <= yground)
        {
            KeepWalking();
        }
    }

    void FlyTowardsPlayer()
    {
        if (player != null)
        {
            // Get the player's position
            Vector3 playerPosition = player.transform.position;

            // Calculate the distance between the player and the flying zombie
            float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

            // If Enemy is close enough, drop down
            if (distanceToPlayer < 8f)
            {
                Destroy(Balloon);
                Destroy(Strings);
                rb.useGravity = true;
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