using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingSlime : MonoBehaviour
{


    public float VikingHealth = 50f;
    private Health currvikingHealth;
    public int damage = 5;
    
    [SerializeField] private float attackCooldown = 1.0f;
    private float lastAttackCooldown = 0.0f;
    void Start()
    {
        CardUnit cardUnit = GetComponent<CardUnit>();
        cardUnit.LevelUpEvent += OnCardUnitLevelUp;
        currvikingHealth = GetComponent<Health>();
    }
    
    
    private void OnCardUnitLevelUp(int newLevel)
    {

        if (newLevel == 2)
        {
            currvikingHealth.health += VikingHealth;
            print(currvikingHealth.health);
        }
        else if (newLevel == 3)
        {
            currvikingHealth.health += VikingHealth;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        print("touching:" + other.gameObject.tag);
        if (other.gameObject.CompareTag("Enemy") && Time.time - lastAttackCooldown >= attackCooldown)
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);
            lastAttackCooldown = Time.time;
        }
    }
}
