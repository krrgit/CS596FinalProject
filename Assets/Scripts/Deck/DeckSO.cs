using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;



public enum Cards
{
    BasicSlime,
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
    public int minAttack = 1;
    public int maxAttack = 9;
    public CardClass[] cards;
}
    

[System.Serializable]
public class CardClass
{
    public Cards card;
    public int attack = 3;
    public GameObject prefab;
    public CardClass(Cards _card, int _attack, GameObject _prefab)
    {
        card = _card;
        attack = _attack;
        prefab = _prefab;
    }

    public override string ToString()
    {
        return card.ToString() + "(" + attack + ")";
    }
    
}
