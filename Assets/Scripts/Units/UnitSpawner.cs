using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Spawns unit on field, given a class & grid cell.
public class UnitSpawner : MonoBehaviour
{
    public static UnitSpawner Instance;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    
    public void SpawnUnit(CardSO cardSO, gridCell gridCell)
    {
        gridCell.isOpen = false;
        var go = Instantiate(cardSO.unitPrefab, gridCell.GetCellPosition(), cardSO.unitPrefab.transform.rotation);
        var card = go.GetComponent<CardUnit>();
        go.transform.parent = transform;
        card.cardSO = cardSO;
        card.gridCell = gridCell;
        gridCell.SetOccupyingUnit(card);
    }
}
