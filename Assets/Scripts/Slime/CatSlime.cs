using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSlime : MonoBehaviour
{
    // Start is called before the first frame update
    private BulletPierce bulletPierce;
    public GameObject bulletPrefab;
    
    /*void Start()
    {
        CardUnit cardUnit = GetComponent<CardUnit>();
        cardUnit.LevelUpEvent += OnCardUnitLevelUp;
        bulletPierce = bulletPrefab.GetComponent<BulletPierce>();
        bulletPierce.maxPierceCount = 1;
    }

    private void Update()
    {  
        print(bulletPierce.maxPierceCount);
    }

    private void OnCardUnitLevelUp(int newLevel)
    {
        //bullet = GetComponent<BulletPierce>();
        if (newLevel == 2)
        {
            bulletPierce.maxPierceCount += 1;
        }
        else if (newLevel == 3)
        {
            bulletPierce.maxPierceCount += 1;
        }
    }*/
}
