using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds a single card on mouse position
public class UICardHolder : MonoBehaviour
{
    [SerializeField] private bool cardHeld;
    [SerializeField] private float minDist = 0.98f;
    [SerializeField] private UICard uiCard;

    private int cardLayer = 0;
    
    private Transform cameraTransform;
    
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
        UpdateCardPosition();
    }

    void Click()
    {
        // check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            if (cardHeld)
            {
                // Check if valid spawn position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                // Hit Something
                if (Physics.Raycast(ray, out hit))
                {
                    gridCell cell = hit.transform.gameObject.GetComponent<gridCell>();
                    // hit cell & is open
                    if (cell && cell.isOpen)
                    {
                        print("Spawn Card");
                        UnitSpawner.Instance.SpawnUnit(uiCard.CardClass, cell);
                        cardHeld = false;
                        uiCard.PlayCard();
                        uiCard = null;
                        UIDeckManager.Instance.DecrementCardCount();
                    } 
                    else
                    {
                        // Return to hand
                        cardHeld = false;
                        uiCard.ReturnCardToHand();
                        uiCard = null;
                    }
                }
                else
                {
                    // Return to hand
                    cardHeld = false;
                    uiCard.ReturnCardToHand();
                    uiCard = null;
                }
            }
            else
            {
                // raycast from mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // check if a card is clicked
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && hit.collider.CompareTag("UICard"))
                    {
                        print("Click Card");
                        cardHeld = true;
                        uiCard = hit.collider.gameObject.GetComponent<UICard>();
                        uiCard.HoldCard();
                    }
                }
            }
        }
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
}

