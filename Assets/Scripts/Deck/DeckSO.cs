using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;



public enum Cards
{
    BasicSlime = 0,
    Chest,
    BunnySlime,
    ArmorSlime,
    HeavyArmorSlime,
    QueenSlime,
    CatSlime,
    SaplingSlime,
    PlantSlime,
    BigCatSlime,
    CardCount
};

[CreateAssetMenu(fileName = "Deck", menuName = "Deck/DeckSO", order = 2)]
public class DeckSO : ScriptableObject
{
    public int deckSize = 10;
    public CardSO[] cardpack;
    public CardSO[] cards;
}

//
// [System.Serializable]
// public class CardClass
// {
//     public Cards card;
//     public int cost = 3;
//     public int health = 100;
//     public CardClass(Cards _card, int _cost)
//     {
//         card = _card;
//         cost = _cost;
//     }
//
//     public override string ToString()
//     {
//         return card.ToString() + "(" + cost + ")";
//     }
//     
// }
