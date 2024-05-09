using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMonsterAI : MonoBehaviour
{
    [SerializeField] private MonsterDetectUnit detectUnit;
    [SerializeField] private Collider collider;
    [SerializeField] private enemyAI enemyAI;
    [SerializeField] private bool usedPhase = false;
    [SerializeField] private float phaseTime = 1.3f;
    [SerializeField] private Renderer meshRenderer;

    private Health unitHealth;

    bool unitDetected;
    private bool isPhasing;
    
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
        if (!usedPhase)
        {
            // Phase through
            usedPhase = true;
            StartCoroutine(IPhase());
        }
        else
        {
            // Attack
            unitDetected = true;
            this.unitHealth = unitHealth;
        }
    }
    
    private void LateUpdate()
    {
        if (isPhasing) return;
        
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

    IEnumerator IPhase()
    {
        print("Phase");
        isPhasing = true;
        collider.enabled = false;
        Color startColor = meshRenderer.material.color;
        Color phaseColor = startColor;
        phaseColor.a = 0.5f;

        meshRenderer.material.color = phaseColor;
        
        Vector3 startPos = transform.position;
        float dist = 0;
        float travelDist = phaseTime;
        while (dist < travelDist)
        {
            dist = Vector3.Distance(startPos, transform.position);
            yield return new WaitForEndOfFrame();
        }

        meshRenderer.material.color = startColor;
        collider.enabled = false;
        isPhasing = false;
    }
    
}
