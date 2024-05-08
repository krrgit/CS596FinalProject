using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of player movement

    // Update is called once per frame
    void Update()
    {
        // Movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

        // Move the player
        transform.Translate(movement);

        // Check for collisions with objects that have a collider
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f); // Check a small sphere around the player
        foreach (Collider collider in colliders)
        {
            // Check if the collided object has a Health component
            Health health = collider.GetComponent<Health>();
            if (health != null)
            {
                // Deal damage to the collided object
                health.TakeDamage(10);
            }

            // Check if the collided object has a ShieldHealth component
            ShieldHealth shieldHealth = collider.GetComponent<ShieldHealth>();
            if (shieldHealth != null)
            {
                // Deal damage to the collided object
                shieldHealth.TakeDamage(10);
            }
        }
    }
}
