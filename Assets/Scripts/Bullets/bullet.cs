using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    public Vector3 shootDirection;
    public float bulletSpeed = 1.0f;
    public float bulletDamage = 10.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(shootDirection * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth) enemyHealth.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
