using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float startHealth = 100;
    public float DPS = 10; // Change it whenever for enemy/slime attack
    public bool godMode = false; // toggle

    void Start()
    {
        health = startHealth;
    }

    public void TakeDamage(float DPS)
    {
        if (!godMode) // Check if god mode is not enabled
        {
            health -= DPS;

            if (health <= 0)
            {
                Death();
            }
        }
    }

    // Death
    private void Death()
    {
       Destroy(gameObject);
    }

    // Toggle for GodMode
    public void ToggleGodMode(bool enabled)
    {
        godMode = enabled;
    }
    
}
