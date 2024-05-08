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
            thisDeck.cards = new CardSO[thisDeck.deckSize];
            for (int i = 0; i < thisDeck.deckSize; i++)
            {
                thisDeck.cards[i] = RandomCard(thisDeck);
            }
        }
    }

    CardSO RandomCard(DeckSO deck)
    {
        return deck.cardpack[Random.Range(0, deck.cardpack.Length)];
    }
}