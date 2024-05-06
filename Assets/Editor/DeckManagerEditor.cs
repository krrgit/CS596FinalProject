using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DeckManager))]
public class DeckManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DeckManager deckManager = (DeckManager)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Initialize Scene Deck(Debug Only)"))
        {
            deckManager.InitializeSceneDeck();
        }
        if (GUILayout.Button("Shuffle Deck"))
        {
            deckManager.ShuffleDeck();
        }

        if (GUILayout.Button("Draw Cards"))
        {
            deckManager.DrawToFillHand();
        }
        if (GUILayout.Button("Draw Card"))
        {
            deckManager.DrawCard();
        }
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("Clear All"))
        {
            deckManager.ClearAll();
        }
    }
}