using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public float startHealth = 100;
    [SerializeField] private GameObject hitEffect;

    private void Start()
    {
        currentHealth = startHealth;
    }
    public void TakeDamage(float damage)
    {
        HitEffect();
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

    void HitEffect()
    {
        if (!hitEffect) return;

        Instantiate(hitEffect, transform.position + Vector3.up, Quaternion.identity);
    }
}
