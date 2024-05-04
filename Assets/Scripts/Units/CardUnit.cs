using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the actual card on the field.
public class CardUnit : MonoBehaviour
{
    public CardClass cardClass;
    public gridCell gridCell;
    [SerializeField] private Health health;
    

    private void OnEnable()
    {
        health.OnDeathEvent += SendBackToDeck;
    }
    
    private void OnDisable()
    {
        health.OnDeathEvent -= SendBackToDeck;
    }

    public void SendBackToDeck()
    {
        gridCell.isOpen = true;
        DeckManager.Instance.ReturnFieldCardToDeck(cardClass);
    }
}
