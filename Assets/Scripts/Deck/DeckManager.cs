using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DeckManager: Manages the deck and the hand in the scene.
public class DeckManager : MonoBehaviour
{
    [SerializeField] private  DeckSO deckSO;                                        // Actual deck. DO NOT MODIFY. 
    [Header("Selection Indices")]
    [SerializeField] private int selectedCardInHand = -1;
    [SerializeField] private int selectedCardOnField = -1;
    [Header("Parameters")]
    [SerializeField] private int handSize = 5;                                      // Max number of cards in hand. 
    [SerializeField] private bool enableStaleness = true;                           // Everytime this card returns to the deck, -1 to attack 
    [Header("Lists")]
    [SerializeField] private List<CardClass> sceneDeck = new List<CardClass>();   // Scene instance of the deck. This is to avoid changing the actual deck.
    [SerializeField] private List<CardClass> cardsInHand = new List<CardClass>();   // List of cards in hand
    [SerializeField] private List<CardClass> cardsOnField = new List<CardClass>();  // List of cards on field

    public static DeckManager Instance; 
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    
    // Returns the current index of the selected card in hand.
    public int GetSelectedCardInHand()
    {
        return selectedCardInHand;
    }
    
    // Set the index of the selected card in hand.
    public void SetSelectedCardInHand(int index)
    {
        if (index >= cardsInHand.Count)
        {
            print("SetSelectedCardInHand: index out of range (" + index + ">=" + cardsInHand.Count);
            return;
        }

        selectedCardInHand = index;
        print("Selected " + cardsInHand[index].ToString());
    }
    
    // Returns the current index of the selected card on field.
    public int GetSelectedCardOnField()
    {
        return selectedCardOnField;
    }
    
    // Set the index of the selected card on field.
    public void SetSelectedCardOnField(int index)
    {
        if (index >= cardsInHand.Count)
        {
            print("SetSelectedCardOnField: index out of range (" + index + ">=" + cardsInHand.Count);
            return;
        }

        selectedCardOnField = index;
        print("Selected " + cardsOnField[index].ToString());
    }
    
    // Play Card onto field.
    // Returns the card played.
    // Store the CardClass because you'll need it for ReturnFieldCardToDeck(CardClass card).
    public CardClass PlayCard()
    {
        if (selectedCardInHand >= cardsInHand.Count || selectedCardInHand < 0)
        {
            print("PlayCard: index out of range (" + selectedCardInHand + ">=" + cardsInHand.Count);
            return null;
        }
        
        CardClass card = cardsInHand[selectedCardInHand];
        cardsInHand.RemoveAt(selectedCardInHand);
        
        cardsOnField.Add(card);
        print("Played " + card.ToString());
        return card;
    }

    // Returns the field card back to the deck.
    public void ReturnFieldCardToDeck(CardClass card)
    {
        if (selectedCardOnField >= cardsOnField.Count || selectedCardOnField < 0)
        {
            print("ReturnFieldCardToDeck: index out of range (" + selectedCardInHand + ">=" + cardsInHand.Count);
            return;
        }
        cardsOnField.Remove(card);
        if (enableStaleness) card.attack--;
        
        sceneDeck.Add(card);
        print("Returned " + card.ToString() + " to deck.");
    }
    
    // Note: only call after setting selectedCardOnField.
    // Would be better to use ReturnFieldCardToDeck(CardClass card)
    // As this could cause issues if multiple cards are returned at once.
    public void ReturnSelectedFieldCardToDeck()
    {
        CardClass card = cardsOnField[selectedCardOnField];
        cardsOnField.Remove(card);
        if (enableStaleness) card.attack--;
        
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
        if (cardsInHand.Count >= handSize || sceneDeck.Count == 0)
        {
            print("DrawCard: Deck does not have cards or hand is full.");
            return false;
        }
        CardClass card = sceneDeck[0];
        sceneDeck.RemoveAt(0);

        cardsInHand.Add(card);
        print("Draw " + card.ToString());
        return true;
    }

    public void DrawToFillHand()
    {
        while (DrawCard()) {}
    }

    public bool ReturnHandCardToDeck(int cardHandIndex)
    {
        if (cardsInHand.Count <= 0) return false;
        CardClass card = cardsInHand[cardHandIndex];
        cardsInHand.RemoveAt(cardHandIndex);
        
        sceneDeck.Add(card);
        print("Returned " + card.ToString() + " to deck.");
        return true;
    }

    public void ReturnFullHandToDeck()
    {
        while (ReturnHandCardToDeck(0)) {}
    }
    
    
    // Clear hand,deck, and field.
    public void ClearAll()
    {
        sceneDeck.Clear();
        cardsInHand.Clear();
        cardsOnField.Clear();
    }

    // For Debugging only
    public void InitializeSceneDeck()
    {
        sceneDeck = new List<CardClass>(deckSO.cards);
    }

    void Start()
    {
        sceneDeck = new List<CardClass>(deckSO.cards);
        ShuffleDeck();
        DrawToFillHand();
    }
    
    
    
    
}
