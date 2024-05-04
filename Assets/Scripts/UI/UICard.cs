using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI Card
public class UICard : MonoBehaviour, IComparable
{
    const int IGNORE_RAYCAST_LAYER =  2;

    [SerializeField] CardClass cardClass;
    [SerializeField] private bool cardHeld;
    [SerializeField] private float moveLerp = 3.0f;
    
    private Vector3 targetPos;

    private Vector3 handPos;

    private int cardLayer;

    public CardClass CardClass
    {
        get { return cardClass; }
    }

    public bool IsHeld
    {
        get { return cardHeld; }
    }

    public void SetCardClass(CardClass _cardClass)
    {
        cardClass = _cardClass;
    }
    // Start is called before the first frame update
    void Start()
    {
        handPos = transform.position;
        targetPos = handPos;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCard();
    }

    void MoveCard()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveLerp);
    }

    public void SetTargetPos(Vector3 newPosition)
    {
        targetPos = newPosition;
    }

    public void ResetPosition()
    {
        targetPos = handPos;
    }

    public void SetHandPosition(Vector3 newPosition)
    {
        handPos = newPosition;
    }

    public void HoldCard()
    {
        cardLayer = gameObject.layer;
        gameObject.layer = IGNORE_RAYCAST_LAYER;
    }

    public void PlayCard()
    {
        Destroy(gameObject);
    }

    public void ReturnCardToHand()
    {
        gameObject.layer = cardLayer;
        ResetPosition();
    }
    
    public int CompareTo(object obj)
    {
        var a = this;
        var b = obj as UICard;
     
        if (a.transform.position.x < b.transform.position.x)
            return -1;
     
        if (a.transform.position.x > b.transform.position.x)
            return 1;
 
        return 0;
    }
}
