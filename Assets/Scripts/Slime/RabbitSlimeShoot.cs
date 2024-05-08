using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSlimeShoot : MonoBehaviour
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
    public float longerSlow = 0;
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
        
        BulletSlow bulletScript = newBullet.GetComponent<BulletSlow>();
        bulletScript.slowDuration += longerSlow;
        print("slow duration: " + bulletScript.slowDuration);

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
            longerSlow = 2f;
        }
        else if (newLevel == 3)
        {
            longerSlow = 4f;
        }
    }
}
