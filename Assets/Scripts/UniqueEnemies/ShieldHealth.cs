using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : MonoBehaviour
{
    public int currentHealth;
    public int startHealth = 100;

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
        currentHealth = startHealth;
        shieldRenderer = Shield.GetComponent<Renderer>();
        shieldCollider = Shield.GetComponent<Collider>();
        color = shieldRenderer.material.color;

        groundCollider = GameObject.FindWithTag("Ground").GetComponent<Collider>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Took " + damage + " damage");

        if (currentHealth <= 50)
        {
            shieldRenderer.material = damagedShield;
        }

        if (currentHealth <= 0)
        {
            Dying();
        }
    }

    // Dying "Animation"
    private void Dying()
    {
        shieldRenderer.material = destroyedShield;
        // Disable collision with the zombie but keep it with the ground/grass
        Physics.IgnoreCollision(shieldCollider, groundCollider, false);
        Invoke("Death", 2f); // Dies after 2 seconds
    }


    // Death
    private void Death()
    {
        Destroy(gameObject);
    }
}
