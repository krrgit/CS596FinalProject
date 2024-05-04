using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DeckManager: Manages the deck and the hand in the scene.
public class DeckManager : MonoBehaviour
{
    [SerializeField] private  DeckSO deckSO;                                        // Actual deck. DO NOT MODIFY. 
    [Header("UI")] 
    [SerializeField] private UIDeckManager uiDeck;
    [Header("Parameters")]
    public int handSize = 5;                                      // Max number of cards in hand. 
    [SerializeField] private bool enableStaleness = true;                           // Everytime this card returns to the deck, -1 to attack 
    [Header("Lists")]
    [SerializeField] private List<CardClass> sceneDeck = new List<CardClass>();   // Scene instance of the deck. This is to avoid changing the actual deck.

    public static DeckManager Instance; 
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    

    // Returns the field card back to the deck.
    public void ReturnFieldCardToDeck(CardClass card)
    {
        sceneDeck.Add(card);
        print("Returned " + card.ToString() + " to deck.");
    }
    
    public void ShuffleDeck()
    {
        List<CardClass> cardsList = new List<CardClass>();

        // Randomly insert into list
        int insertIndex = 0;
        while (sceneDeck.Count > 0)
        {
            CardClass card = sceneDeck[0];
            sceneDeck.RemoveAt(0);
            cardsList.Insert(insertIndex,card);
            insertIndex = Random.Range(0, cardsList.Count);
        }

        sceneDeck = new List<CardClass>(cardsList.ToArray());
        print("Shuffled deck.");
    }

    public bool DrawCard()
    {
        if (sceneDeck.Count == 0)
        {
            print("DrawCard: Deck is out of cards.");
            return false;
        }

        CardClass card = sceneDeck[0];
        if (UIDeckManager.Instance.SpawnCard(card))
        {
            sceneDeck.RemoveAt(0);
            print("Draw " + card.ToString());
            return true;
        }
        return false;
    }

    public void DrawToFillHand()
    {
        while (DrawCard()) {}
    }
    
    // Clear hand,deck, and field.
    public void ClearAll()
    {
        sceneDeck.Clear();
    }

    // For Debugging only
    public void InitializeSceneDeck()
    {
        sceneDeck = new List<CardClass>(deckSO.cards);
    }

    void Start()
    {
        sceneDeck = new List<CardClass>(deckSO.cards);
        InitializeSceneDeck();
        ShuffleDeck();
        DrawToFillHand();
    }
    
    
    
    
}
