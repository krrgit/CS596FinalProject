using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Deck/CardSO", order = 2)]
public class CardSO : ScriptableObject
{
    public string cardName;
    public int cost = 1;
    public int health = 3;
    public int attack = 2;
    public int level = 1;
    public string description = "This is a sample description of a card.";
    public GameObject unitPrefab; // The prefab that gets spawned on the field
    public GameObject facePrefab;  // The prefab that is displayed on the face of the card
}