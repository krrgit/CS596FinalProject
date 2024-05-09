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
    private Camera mainCam;

    [SerializeField] private bool dropOnRelease;

    private UIDeckManager uiDeckManager;
    
    public static UICardHolder Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        uiDeckManager = UIDeckManager.Instance;
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
                        if (TryPickupCard(hit.collider))
                        {
                            return;
                        } 
                        else if (TryRedrawCards(hit.collider))
                        {
                            return;
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

    private bool TryPickupCard(Collider hit)
    {
        if (hit.CompareTag("UICard"))
        {
            print("Pickup Card");
            SoundManager.Instance.PlayClip("grabcard"); // play sound fx for grabbing a card

            cardHeld = true;
            uiCard = hit.gameObject.GetComponent<UICard>();
            uiCard.HoldCard();
            StartCoroutine(IHoldTimer());
            return true;
        }

        return false;
    }

    private bool TryRedrawCards(Collider hit)
    {
        if (hit.CompareTag("UIDeck") && gemCollector.GetGemCount() >= 1)
        {
            uiDeckManager.Redraw();
            gemCollector.DecrementGemCount(1);
            return true;
        }

        return false;
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
                    gemCollector.DecrementGemCount(uiCard.CardSO.cost);
                    UnitSpawner.Instance.SpawnUnit(uiCard.CardSO, cell);
                    cardHeld = false;
                    uiCard.PlayCard();
                    UIDeckManager.Instance.DecrementCardCount(uiCard);
                    uiCard = null;
                    return;
                }
                else
                {
                    print("Insufficient Gems.");
                }
            } else if (unit)
            {
                // Check if the same card
                print(unit.cardSO.cardName);
                print(uiCard.CardSO.cardName);
                if (unit.cardSO.cardName == uiCard.CardSO.cardName)
                {
                    // Use Card as EXP
                    if (uiCard.CardSO.cost <= gemCollector.GetGemCount() && unit.AddExp())
                    {
                        gemCollector.DecrementGemCount(uiCard.CardSO.cost);
                        cardHeld = false;
                        uiCard.PlayCard();
                        UIDeckManager.Instance.DecrementCardCount(uiCard);
                        uiCard = null;
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
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
            
        float dist = Mathf.Max(minDist, hit.distance);
        Vector3 cardPos = dist * ray.direction + mainCam.transform.position;

        uiCard.SetTargetPos(cardPos);
    }

    IEnumerator IHoldTimer()
    {
        dropOnRelease = false;
        yield return new WaitForSecondsRealtime(holdTimer);
        if (cardHeld)
        {
            dropOnRelease = true;
        }
    }
}

