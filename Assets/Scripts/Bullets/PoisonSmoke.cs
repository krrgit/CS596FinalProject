using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonSmoke : MonoBehaviour
{

    public int PoisonDamage = 5;
    public float PoisonDamageDuration = 5f;
    public float PoisonDuration = 7f;
    private void Start()
    {
        StartCoroutine(DestroySmokeAfterDelay(gameObject, PoisonDuration));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyAI ai = other.gameObject.GetComponent<enemyAI>();
            if (enemyHealth && ai)
            {
                ApplyPoison(enemyHealth, ai);
            }
        }
    }

    private void ApplyPoison(EnemyHealth enemyHealth, enemyAI ai)
    {
        StartCoroutine(AfflictPoisonDamage(enemyHealth, ai));
    }

    private IEnumerator AfflictPoisonDamage(EnemyHealth enemyHealth, enemyAI ai)
    {
        float timer = 0f;
        if (!ai.isPoisoned)
        {
            while (timer < PoisonDamageDuration && enemyHealth)
            {

                enemyHealth.TakeDamage(PoisonDamage * Time.deltaTime);
                timer += Time.deltaTime;
                ai.isPoisoned = false;
                yield return new WaitForEndOfFrame();
            }
        }
    }
    
    private IEnumerator DestroySmokeAfterDelay(GameObject smoke, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(smoke);
    }
}
