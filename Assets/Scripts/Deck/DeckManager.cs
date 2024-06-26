using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DeckManager: Manages the deck and the hand in the scene.
public class DeckManager : MonoBehaviour
{
    [SerializeField] private  DeckSO deckSO;                                        // Actual deck. DO NOT MODIFY. 
    [Header("Parameters")]
    public int handSize = 5;                                                        // Max number of cards in hand. 
    [SerializeField] private float waitBeforeFirstDraw = 0.5f;
    [SerializeField] private float timeBetweenDraws = 0.1f;
    [SerializeField] private bool isDrawingCards;
    
    [Header("Lists")]
    [SerializeField] private List<CardSO> sceneDeck = new List<CardSO>();     // Scene instance of the deck. This is to avoid changing the actual deck.
    
    
    public static DeckManager Instance;

    public bool IsDrawingCards
    {
        get { return isDrawingCards; }
    }
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    

    // Returns the field card back to the deck.
    public void ReturnFieldCardToDeck(CardSO card)
    {
        sceneDeck.Add(card);
        print("Returned " + card.ToString() + " to deck.");
    }
    
    public void ShuffleDeck()
    {
        List<CardSO> cardsList = new List<CardSO>();

        // Randomly insert into list
        int insertIndex = 0;
        while (sceneDeck.Count > 0)
        {
            CardSO card = sceneDeck[0];
            sceneDeck.RemoveAt(0);
            cardsList.Insert(insertIndex,card);
            insertIndex = Random.Range(0, cardsList.Count);
        }

        sceneDeck = new List<CardSO>(cardsList.ToArray());
        print("Shuffled deck.");
    }

    public bool DrawCard()
    {
        if (sceneDeck.Count == 0)
        {
            print("DrawCard: Deck is out of cards.");
            return false;
        }

        CardSO card = sceneDeck[0];
        if (UIDeckManager.Instance.SpawnCard(card))
        {
            sceneDeck.RemoveAt(0);
            SoundManager.Instance.PlayClip("dealcards"); // play sound fx when dealing cards
            print("Draw " + card.ToString());
            return true;
        }
        return false;
    }

    public void DrawToFillHand()
    {
        StartCoroutine(IDrawToFillHand());
    }

    IEnumerator IDrawToFillHand()
    {
        
        isDrawingCards = true;
        int draws = handSize - UIDeckManager.Instance.CardCount;
        while (draws > 0 && DrawCard())
        {
            yield return new WaitForSecondsRealtime(timeBetweenDraws);
            draws--;
        }

        isDrawingCards = false;
    }
    
    // Clear hand,deck, and field.
    public void ClearAll()
    {
        sceneDeck.Clear();
    }

    // For Debugging only
    public void InitializeSceneDeck()
    {
        sceneDeck = new List<CardSO>(deckSO.cards);
    }

    void Start()
    { 
        StartCoroutine(ISetupDeck());
    }

    IEnumerator ISetupDeck()
    {
        sceneDeck = new List<CardSO>(deckSO.cards);
        InitializeSceneDeck();
        ShuffleDeck();
        
        yield return new WaitForSeconds(waitBeforeFirstDraw);
        DrawToFillHand();
    }
}
