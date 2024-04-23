using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DeckSO))]
public class DeckObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DeckSO thisDeck = (DeckSO)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Randomize Deck"))
        {
            thisDeck.cards = new CardClass[thisDeck.deckSize];
            for (int i = 0; i < thisDeck.deckSize; i++)
            {
                thisDeck.cards[i] = RandomCard(thisDeck);
            }
        }
    }

    CardClass RandomCard(DeckSO deck)
    {
        Cards randomCardType = (Cards)Random.Range(0, (int)Cards.CardCount);
        int randomAttack = Random.Range(deck.minAttack, deck.maxAttack + 1);
        GameObject prefab = GetPrefabForCardType(randomCardType);
        CardClass randomCard = new CardClass(randomCardType, randomAttack, prefab);
        return randomCard;
    }
    
    GameObject GetPrefabForCardType(Cards cardType)
    {
        GameObject prefab = null;

        // Assign prefab based on card type
        switch (cardType)
        {
            case Cards.BasicSlime:
                prefab = Resources.Load<GameObject>("BasicSlimePrefab");
                break;
            case Cards.Chest:
                prefab = Resources.Load<GameObject>("ChestPrefab");
                break;
            default:
                Debug.LogWarning("Prefab not assigned for card type: " + cardType);
                break;
        }

        return prefab;
    }
}