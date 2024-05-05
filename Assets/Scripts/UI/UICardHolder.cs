using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds a single card on mouse position
public class UICardHolder : MonoBehaviour
{
    [SerializeField] private bool cardHeld;
    [SerializeField] private float minDist = 0.98f;    // Minimum distance the card is held from the camera.
    [SerializeField] private float holdTimer = 0.25f;  // Time to hold so that letting go drops the card.
    [SerializeField] private GemCollector gemCollector;
    [SerializeField] private UICard uiCard;
    private Transform cameraTransform;

    [SerializeField] private bool dropOnRelease;
    
    public static UICardHolder Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Click();
        ClickRelease();
        UpdateCardPosition();
    }

    void ClickRelease()
    {
        if (Input.GetMouseButtonUp(0) && cardHeld && dropOnRelease)
        {
            TryDropCard();
        }
    }

    void Click()
    {
        // check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            if (cardHeld)
            {
                TryDropCard();
            }
            else
            {
                // Try pick up or draw card
                // raycast from mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // check if a card/deck is clicked
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.CompareTag("UICard"))
                        {
                            print("Hold Card");
                            cardHeld = true;
                            uiCard = hit.collider.gameObject.GetComponent<UICard>();
                            uiCard.HoldCard();
                            StartCoroutine(IHoldTimer());
                        } 
                        else if (hit.collider.CompareTag("UIDeck"))
                        {
                            UIDeckManager.Instance.Redraw();
                        }
                    }
                }
            }
        }
    }

    void ReturnToHand()
    {
        cardHeld = false;
        uiCard.ReturnCardToHand();
        uiCard = null;
    }

    // Will either spawn the card or return it to hand.
    void TryDropCard()
    {
        // Check if valid spawn position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Hit Something
        if (Physics.Raycast(ray, out hit))
        {
            gridCell cell = hit.transform.gameObject.GetComponent<gridCell>();
            CardUnit unit = hit.transform.gameObject.GetComponent<CardUnit>();
            // hit cell & is open
            if (cell && cell.isOpen)
            {
                // Play Card 
                if (uiCard.CardSO.cost <= gemCollector.GetGemCount())
                {
                    print("Spawn Card");
                    UnitSpawner.Instance.SpawnUnit(uiCard.CardSO, cell);
                    cardHeld = false;
                    uiCard.PlayCard();
                    uiCard = null;
                    UIDeckManager.Instance.DecrementCardCount();
                    return;
                }
                else
                {
                    print("Insufficient Gems.");
                }
            } else if (unit)
            {
                // Check if the same card
                if (unit.cardSO.cardName == uiCard.CardSO.cardName)
                {
                    // Use Card as EXP
                    if (uiCard.CardSO.cost <= gemCollector.GetGemCount() && unit.AddExp())
                    {
                        cardHeld = false;
                        uiCard.PlayCard();
                        uiCard = null;
                        UIDeckManager.Instance.DecrementCardCount();
                        return;
                    }
                }
            }
        }
        // Condition for spawning card failed, return to hand.
        ReturnToHand();
    }

    void UpdateCardPosition()
    {
        if (!uiCard) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
            
        float dist = Mathf.Max(minDist, hit.distance);
        Vector3 cardPos = dist * ray.direction + cameraTransform.position;

        uiCard.SetTargetPos(cardPos);
    }

    IEnumerator IHoldTimer()
    {
        dropOnRelease = false;
        yield return new WaitForSeconds(holdTimer);
        if (cardHeld)
        {
            dropOnRelease = true;
        }
    }
}

