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
    [SerializeField] private float cardSpacing = 1.0f;
    [SerializeField] private float cardsSpaceWidth = 5.0f;
    [SerializeField] private int cardCount = 0;

    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private List<UICard> uiCards = new List<UICard>();
    
    public static UIDeckManager Instance;

    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    
    public bool SpawnCard(CardClass card)
    {
        if (cardCount >= DeckManager.Instance.handSize) return false;
        Vector3 position = firstCardTransform.position + (cardCount * cardSpacing * transform.right);
        var go = Instantiate(cardPrefab, position, firstCardTransform.rotation);
        go.transform.SetParent(cardParent);
        cardCount++;
        
        var uicard = go.GetComponent<UICard>();
        uicard.SetCardClass(card);
        uiCards.Add(uicard);
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

        float xPos = 0;
        foreach (var card in uiCards)
        {
            Vector3 cardPosition = firstCardTransform.position;
            card.SetHandPosition(cardPosition + (xPos * transform.right));
            card.ResetPosition();
            xPos += cardsSpaceWidth / uiCards.Count;
        }
    }
}
