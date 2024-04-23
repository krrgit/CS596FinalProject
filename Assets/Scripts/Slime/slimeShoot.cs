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
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        newBullet.transform.SetParent(transform);

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
}
