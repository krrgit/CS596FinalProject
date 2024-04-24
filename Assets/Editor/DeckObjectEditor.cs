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
        return new CardClass(
            (Cards)Random.Range(0, (int)Cards.CardCount),
            Random.Range(deck.minAttack, deck.maxAttack + 1)
        );
    }
}