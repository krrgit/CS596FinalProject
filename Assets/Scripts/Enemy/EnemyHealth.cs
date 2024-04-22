using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public float startHealth = 100;

    private void Start()
    {
        currentHealth = startHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    // Death
    private void Death()
    {
        Destroy(gameObject);
    }
}
