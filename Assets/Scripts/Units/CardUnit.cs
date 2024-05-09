using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the actual card on the field.
public class CardUnit : MonoBehaviour
{
    public CardSO cardSO;
    public gridCell gridCell;
    [SerializeField] private int level = 1;
    [SerializeField] private int maxLevel = 3;
    [SerializeField] private int currExp = 0;
    [SerializeField] int totalExp = 0;
    [SerializeField] int[] expRequirements = {1, 2, 3};
    [SerializeField] private int[] healthUps = { 1,1,1,1}; // Health increases on level up
    [SerializeField] private Health health;

    public delegate void LevelUpDelegate(int level);
    public LevelUpDelegate LevelUpEvent;
    
    private void OnEnable()
    {
        health.OnDeathEvent += SendBackToDeck;
    }
    
    private void OnDisable()
    {
        health.OnDeathEvent -= SendBackToDeck;
    }

    public void Setup(CardSO cardSO, gridCell gridCell)
    {
        this.cardSO = cardSO;
        this.gridCell = gridCell;
        health.startHealth = cardSO.health;
        health.health = cardSO.health;
    }

    public void SendBackToDeck()
    {
        gridCell.isOpen = true;

        // Add all the cards used as EXP back to the deck.
        while (totalExp >= 0)
        {
            DeckManager.Instance.ReturnFieldCardToDeck(cardSO);
            totalExp--;
        }
    }

    public bool AddExp()
    {
        if (level == maxLevel)
        {
            return false;
        }
        
        currExp++;
        totalExp++;
        if (currExp >= expRequirements[level])
        {
            level++;
            currExp = 0;
            print(cardSO.cardName + " is level " + level + "!");
            
            // Heal Slime
            health.startHealth += healthUps[level];
            health.health = health.startHealth;
            
            LevelUpEvent?.Invoke(level);
        }

        return true;
    }
    
}
