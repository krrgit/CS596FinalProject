using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VikingAttack : MonoBehaviour
{
    [SerializeField] private VikingSlime viking;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            viking.DealDamageCheck(enemyHealth);
        }
    }
}
