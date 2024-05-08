using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPierce : MonoBehaviour
{
    
    public Vector3 shootDirection;
    public float bulletSpeed = 1.0f;
    public float bulletDamage = 10.0f;
    public int maxPierceCount;

    private int currentPierceCount = 0;
    
    void Update()
    {
        
        //print("max pierce count is: " + maxPierceCount);
        transform.Translate(shootDirection * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();

            enemyHealth.TakeDamage(bulletDamage);

            currentPierceCount++;

            if (currentPierceCount >= maxPierceCount)
            {
                Destroy(gameObject);
            }
            
        }
    }

    public void SetMaxPierceCount(int newMaxPierceCount)
    {
        print("new max pierce count: " + newMaxPierceCount);
        maxPierceCount = newMaxPierceCount;
    }
}
