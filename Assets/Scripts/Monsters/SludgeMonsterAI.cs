using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SludgeMonsterAI : MonoBehaviour
{
    [SerializeField] private enemyAI enemyAI;
    [SerializeField] private MonsterDetectUnit detectUnit;

    private Health unitHealth;
    bool unitDetected;
    
    private void OnEnable()
    {
        detectUnit.DetectUnitEvent += UnitDetected;
    }
    private void OnDisable()
    {
        detectUnit.DetectUnitEvent -= UnitDetected;
    }

    void UnitDetected(Health unitHealth)
    {
        this.unitHealth = unitHealth;
        unitDetected = true;
    }
    
    
    private void LateUpdate()
    {
        if (unitHealth)
        {
            //enemyAI.AttackUnit(unitHealth);
            enemyAI.StopAttack();
        }
        else if (unitDetected)
        {
            enemyAI.StopAttack();
            unitDetected = false;
        }
    }
    
}
