using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : MonoBehaviour
{
    private EnemyHealth enemyHealth;

    [SerializeField]
    private GameObject Shield;
    private Renderer shieldRenderer;
    private Collider shieldCollider;
    private Collider groundCollider; 
    private Color color;
    public Material damagedShield; // for DamagedShield
    public Material destroyedShield; // for DestroyedShield

    void Start()
    {
        // Find and assign the EnemyHealth component
        enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth == null)
        {
            Debug.LogError("You broke something good job");
        }

        shieldRenderer = Shield.GetComponent<Renderer>();
        shieldCollider = Shield.GetComponent<Collider>();
        color = shieldRenderer.material.color;

        groundCollider = GameObject.FindWithTag("gridCell").GetComponent<Collider>();
    }

    void Update()
    {
        if (enemyHealth != null)
        {
            // Accessing the currentHealth value
            float health = enemyHealth.currentHealth;
            if (health <= 100)
            {
                shieldRenderer.material = damagedShield;
            }

            if (health <= 50)
            {
                Dying();
            }
        }
    }

    // Dying "Animation"
    private void Dying()
    {
        shieldRenderer.material = destroyedShield;
        // Disable collision with the zombie but keep it with the ground/grass
        Physics.IgnoreCollision(shieldCollider, groundCollider, false);
        Invoke("Death", 3f); // Dies after 2 seconds
    }


    // Death
    private void Death()
    {
        Destroy(gameObject);
    }
}

