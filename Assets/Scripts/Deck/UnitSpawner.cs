using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Spawns unit on field, given a class & grid cell.
public class UnitSpawner : MonoBehaviour
{
    
    public GameObject[] prefabs;

    public static UnitSpawner Instance;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    
    public void SpawnUnit(CardClass cardClass, gridCell gridCell)
    {
        gridCell.isOpen = false;
        var go = Instantiate(prefabs[(int)cardClass.card], gridCell.GetCellPosition(), Quaternion.identity);
        var card = go.GetComponent<CardUnit>();
        card.cardClass = cardClass;
        card.gridCell = gridCell;
    }
}
