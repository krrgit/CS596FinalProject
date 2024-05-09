using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMonsterAI : MonoBehaviour
{
    [SerializeField] private enemyAI enemyAI;
    [SerializeField] private MonsterDetectUnit detectUnit;
    [SerializeField] private Health unitHealth;
    
    private bool unitDetected;

    private void OnEnable()
    {
        detectUnit.DetectUnitEvent += SetUnitToAttack;
    }
    private void OnDisable()
    {
        detectUnit.DetectUnitEvent -= SetUnitToAttack;
    }

    void SetUnitToAttack(Health unitHealth)
    {
        this.unitHealth = unitHealth;
        unitDetected = true;
    }

    private void LateUpdate()
    {
        if (unitHealth)
        {
            enemyAI.AttackUnit(unitHealth);
        }
        else if (unitDetected)
        {
            enemyAI.StopAttack();
            unitDetected = false;
        }
    }
}
