using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int shotsPerBurst = 3;
    public float timeBetweenShots = 0.5f;
    public float burstDelay = 2.0f;

    private bool canShoot = true;
    public float atkSpeedMultiplier;
    public int damageBonus;
    public int baseDamage;

    public bool canBuff = false;
    private bool isLvl2 = false;
    private bool isLvl3 = false;
    private CardUnit cardUnit;

    void Start()
    {
        cardUnit = GetComponent<CardUnit>();
        cardUnit.LevelUpEvent += OnCardUnitLevelUp;
        baseDamage = cardUnit.cardSO.attack;
    }
    void Update()
    {

        if (canShoot)
        {
            StartCoroutine(ContinuousShoot());
        }
    }

    IEnumerator ContinuousShoot()
    {
        canShoot = false;

        while (gameObject.activeInHierarchy)
        {
            for (int i = 0; i < shotsPerBurst; i++)
            {
                Shoot();
                yield return new WaitForSeconds(timeBetweenShots);
            }

            yield return new WaitForSeconds(burstDelay);
        }

        canShoot = true;
    }

    void Shoot()
    {
        
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        newBullet.transform.SetParent(transform);

        SoundManager.Instance.PlayClip("shoot"); // play sound fx when slimes shoot


        bullet bulletScript = newBullet.GetComponent<bullet>();
        bulletScript.bulletDamage = baseDamage;

        if (canBuff)
        {
            bulletScript.bulletDamage += damageBonus;
            bulletScript.bulletSpeed += atkSpeedMultiplier;
        }

        StartCoroutine(DestroyBulletWhenOutOfView(newBullet));
    }
    
    //destroys instantiated bullets if out of view
    IEnumerator DestroyBulletWhenOutOfView(GameObject bullet)
    {
        Camera mainCamera = Camera.main;

        while (bullet != null)
        {
            Vector3 viewportPos = mainCamera.WorldToViewportPoint(bullet.transform.position);

            if (viewportPos.x < 0f || viewportPos.x > 1f || viewportPos.y < 0f || viewportPos.y > 1f)
            {
                Destroy(bullet);
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
    
    private void OnCardUnitLevelUp(int newLevel)
    {

        if (newLevel == 2)
        {
            isLvl2 = true;
            shotsPerBurst += 1;
        }
        else if (newLevel == 3)
        {
            shotsPerBurst += 1;
            isLvl3 = true;
            isLvl2 = false;
        }
    }

}
