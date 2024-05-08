using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSlimeShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int shotsPerBurst = 3;
    public float timeBetweenShots = 0.5f;
    public float burstDelay = 2.0f;

    private bool canShoot = true;
    public float atkSpeedMultiplier;
    public int damageBonus;

    public bool canBuff = false;
    private bool isLvl2 = false;
    private bool isLvl3 = false;
    private CardUnit cardUnit;

    public int additionalPierceCount = 0;
    void Start()
    {
        cardUnit = GetComponent<CardUnit>();
        cardUnit.LevelUpEvent += OnCardUnitLevelUp;
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

        BulletPierce bulletScript = newBullet.GetComponent<BulletPierce>();
        bulletScript.maxPierceCount += additionalPierceCount;
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

            yield return null;
        }
    }
    
    private void OnCardUnitLevelUp(int newLevel)
    {

        if (newLevel == 2)
        {
            isLvl2 = true;
            shotsPerBurst += 1;
            additionalPierceCount = 1;
        }
        else if (newLevel == 3)
        {
            shotsPerBurst += 1;
            isLvl3 = true;
            isLvl2 = false;
            additionalPierceCount = 100;
        }
    }

}
