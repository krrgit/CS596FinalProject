using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSlow : MonoBehaviour
{
    public Vector3 shootDirection;
    public float bulletSpeed = 10.0f;
    public float bulletDamage = 10.0f;
    public float slowDuration = 2.0f;
    public float slowFactor = 0.5f; // Factor to slow enemy's movement speed
    public float bulletForce = 1f;
    private Rigidbody rb;
    private bool hasHitEnemy = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Calculate initial velocity using shootDirection, bulletSpeed, and bulletForce
        Vector3 initialVelocity = shootDirection * bulletSpeed * bulletForce;
        rb.velocity = initialVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Colliding with: " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Enemy") && !hasHitEnemy)
        {
            hasHitEnemy = true;

            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();

            enemyHealth.TakeDamage(bulletDamage);
            
            enemyAI enemyAI = other.gameObject.GetComponent<enemyAI>();

            enemyAI.ApplySlowEffect(slowDuration, slowFactor);
                
            

            Destroy(gameObject);
        }
        else if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Gem")) // Ignore collisions with player and walls
        {
            Bounce();
        }
    }

    private void Bounce()
    {
        // Raycast to detect ground or wall in the direction of the bullet's velocity
        RaycastHit hit;
        if (Physics.Raycast(transform.position, rb.velocity.normalized, out hit, 1f))
        {
            // Reflect the bullet's velocity upon collision with ground/wall
            rb.velocity = Vector3.Reflect(rb.velocity, hit.normal);
            SoundManager.Instance.PlayClip("collectgem"); // play sound fx when bullet bounces

        }
    }
}
