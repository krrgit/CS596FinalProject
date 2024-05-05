using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Spawns unit on field, given a class & grid cell.
public class UnitSpawner : MonoBehaviour
{
    public GameObject[] cardDisplays;   // These are the models used for the card faces.
    public static UnitSpawner Instance;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    
    public void SpawnUnit(CardSO cardSO, gridCell gridCell)
    {
        gridCell.isOpen = false;
        var go = Instantiate(cardSO.prefab, gridCell.GetCellPosition(), Quaternion.identity);
        var card = go.GetComponent<CardUnit>();
        go.transform.parent = transform;
        card.cardSO = cardSO;
        card.gridCell = gridCell;
    }
    
    public GameObject GetCardDisplay(string cardName)
    {
        for (int i = 0; i < cardDisplays.Length; i++)
        {
            if (cardDisplays[i].name.Contains(cardName))
            {
                return cardDisplays[i];
            }
        }

        return null;
    }
}