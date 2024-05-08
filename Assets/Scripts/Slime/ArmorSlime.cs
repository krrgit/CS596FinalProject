using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSlime : MonoBehaviour
{
    public float ArmorHealth = 50f;
    private Health currArmorHealth;


    void Start()
    {
        CardUnit cardUnit = GetComponent<CardUnit>();
        cardUnit.LevelUpEvent += OnCardUnitLevelUp;
        currArmorHealth = GetComponent<Health>();
    }
    
    
    private void OnCardUnitLevelUp(int newLevel)
    {

        if (newLevel == 2)
        {
            currArmorHealth.health += ArmorHealth;
        }
        else if (newLevel == 3)
        {
            currArmorHealth.health += ArmorHealth;
        }
    }
    
}
