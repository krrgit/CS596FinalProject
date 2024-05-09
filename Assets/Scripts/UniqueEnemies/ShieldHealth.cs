using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHealth : MonoBehaviour
{
    private EnemyHealth enemyHealth;

    [SerializeField]
    private GameObject Shield;

    void Start()
    {
        // Find and assign the EnemyHealth component
        enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth == null)
        {
            Debug.LogError("You broke something good job");
        }
    }

    void Update()
    {
        if (enemyHealth != null)
        {
            // Accessing the currentHealth value
            float health = enemyHealth.currentHealth;
            float halfhealth = enemyHealth.startHealth;

            if (health <= halfhealth/2)
            {
                Invoke("Death", 3f); // Dies after 2 seconds
                Destroy(Shield);
            }
        }
    }
}

