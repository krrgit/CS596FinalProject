using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCatSlimeShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int shotsPerBurst = 3;
    public float timeBetweenShots = 0.5f;
    public float burstDelay = 2.0f;

    private bool canShoot = true;

    public bool isLvl2 = false;
    public bool isLvl3 = false;
    private CardUnit cardUnit;
    private BulletPoison bullet;
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
        
        SoundManager.Instance.PlayClip("shoot"); // play sound fx when slimes shoot

        BulletPoison bulletScript = newBullet.GetComponent<BulletPoison>();
        

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
        }
        else if (newLevel == 3)
        {
            isLvl3 = true;
        }
    }
}
