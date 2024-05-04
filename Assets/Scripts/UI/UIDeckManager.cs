using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the Deck UI
public class UIDeckManager : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] Transform firstCardTransform;
    [SerializeField] private Transform cardParent;
    [SerializeField] private Transform cardSpawnPoint;
    [Header("Layout")]
    [SerializeField] private float cardSpacing = 1.0f;
    [SerializeField] private int cardCount = 0;
    
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private List<UICard> uiCards = new List<UICard>();
    
    public static UIDeckManager Instance;

    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    
    // Adds a card from the deck to hand
    public bool SpawnCard(CardSO card)
    {
        if (cardCount >= DeckManager.Instance.handSize) return false;

        Vector3 position = cardSpawnPoint.position;
        var go = Instantiate(cardPrefab, position, cardSpawnPoint.rotation);
        go.transform.SetParent(cardParent);
        cardCount++;

        Vector3 cardPosition = firstCardTransform.localPosition;
        cardPosition.x = (cardSpacing * 0.5f) -(cardSpacing * uiCards.Count) / 2.0f;
        firstCardTransform.localPosition = cardPosition;

        var uicard = go.GetComponent<UICard>();
        uicard.SetCardClass(card);
        uicard.SetHandPosition(firstCardTransform.position);
        uicard.ResetPosition();
        uicard.FlipCard(firstCardTransform.rotation);
        
        uiCards.Insert(0,uicard);
        UpdateCardPositions();
        return true;
    }

    public void DecrementCardCount()
    {
        cardCount--;
    }

    private void OnTriggerEnter(Collider other)
    {
        var card = other.GetComponent<UICard>();
        if (card && !uiCards.Contains(card))
        {
            int index = GetHandPosIndex(card.transform.localPosition.x);
            uiCards.Insert(index, card);
            UpdateCardPositions();
            print("Add Card at " + index);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var card = other.GetComponent<UICard>();
        if (card && uiCards.Contains(card))
        {
            
            uiCards.Remove(card);
            UpdateCardPositions();
            print("Remove Card");
        }
    }

    int GetHandPosIndex(float xPos)
    {
        int i = 0;
        foreach (var card in uiCards)
        {
            if (xPos < card.transform.localPosition.x)
            {
                print("card on mouse: " + xPos + " | card on board: " + card.transform.localPosition.x);
                return i;
            }
            i++;
        }
        return uiCards.Count;
    }

    void UpdateCardPositions()
    {
        Vector3 firstCardPosition = firstCardTransform.localPosition;
        firstCardPosition.x = (cardSpacing * 0.5f) -(cardSpacing * uiCards.Count) / 2.0f;
        firstCardTransform.localPosition = firstCardPosition;
        
        float xPos = 0;
        foreach (var card in uiCards)
        {
            Vector3 cardPosition = firstCardTransform.position;
            card.SetHandPosition(cardPosition + (xPos * transform.right));
            card.ResetPosition();
            xPos += cardSpacing;
        }
    }
}
