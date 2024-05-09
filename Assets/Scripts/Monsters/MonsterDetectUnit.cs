using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetectUnit : MonoBehaviour
{
    public delegate void DetectUnitDelegate(Health unitHealth);
    public DetectUnitDelegate DetectUnitEvent;
    public delegate void LeaveUnitDelegate();
    public LeaveUnitDelegate LeaveUnitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var unitHealth = other.GetComponent<Health>();
            DetectUnitEvent?.Invoke(unitHealth);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LeaveUnitEvent?.Invoke();
        }
    }
}
