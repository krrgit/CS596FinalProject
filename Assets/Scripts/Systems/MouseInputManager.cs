using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputManager : MonoBehaviour
{
    private DeckManager deckManager;
    [SerializeField] private PrefabSpawner prefabSpawner;
    void Start()
    {
        deckManager = DeckManager.Instance;
    }
    
    
    //this method is called when you want to field a unit onto the grid
    public void CellClick(gridCell cell)
    {
        if (cell.isHighlighted)
        {
            //gets cell position from grid
            Vector3 cellPosition = cell.GetCellPosition();
            //play a card from hand
            CardClass playedCard = deckManager.PlayCard();
            cellPosition.y = 1.0f;
            if (playedCard != null)
            {
                //get the prefab associated with the played cards enum
                GameObject prefab = prefabSpawner.GetPrefab(playedCard.card);
                if (prefab != null)
                {
                    //place cards prefab at the specified location
                    Instantiate(prefab, cellPosition, Quaternion.identity);
                    //return the card back to the deck after being fielded
                    deckManager.ReturnFieldCardToDeck(playedCard);
                }
                else
                {
                    Debug.LogError("Prefab not found for card: " + playedCard.card);
                }
            }
        }
    }
}
