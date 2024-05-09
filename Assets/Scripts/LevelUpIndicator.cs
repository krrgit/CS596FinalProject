using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;

public class LevelUpIndicator : MonoBehaviour
{
    [SerializeField] CardUnit cardUnit;
    public GameObject lvl2aura;
    public GameObject lvl3aura;
    void OnEnable()
    {
        cardUnit.LevelUpEvent += OnCardUnitLevelUp;
    }
    
    void OnDisable()
    {
        cardUnit.LevelUpEvent -= OnCardUnitLevelUp;
    }
    
    private void OnCardUnitLevelUp(int newLevel)
    {

        if (newLevel == 2)
        {
            lvl2aura.SetActive(true);
        }
        else if (newLevel == 3)
        {
            lvl2aura.SetActive(false);
            lvl3aura.SetActive(true);
        }
    }
}
