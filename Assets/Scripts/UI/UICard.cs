using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// UI Card
public class UICard : MonoBehaviour, IComparable
{
    const int IGNORE_RAYCAST_LAYER =  2;

    [SerializeField] CardSO cardClass;
    [SerializeField] private bool cardHeld;
    [SerializeField] private float moveLerp = 3.0f;
    [SerializeField] private float rotateLerp = 10.0f;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector3 handPos;

    [Header("UI Elements")] 
    [SerializeField] private TMP_Text tmpName;
    [SerializeField] private TMP_Text tmpCost;
    
    private int cardLayer;

    public CardSO CardClass
    {
        get { return cardClass; }
    }

    public bool IsHeld
    {
        get { return cardHeld; }
    }

    public void SetCardClass(CardSO _cardClass)
    {
        cardClass = _cardClass;
        UpdateUI();
    }
    
    // Start is called before the first frame update
    void Awake()
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

    void UpdateUI()
    {
        tmpName.text = InsertSpaceBetweenWords(cardClass.name.ToString());
        tmpCost.text = cardClass.cost.ToString();
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

    public void FlipCard(Quaternion newRot)
    {
        StartCoroutine(IFlipCard(newRot));
    }

    IEnumerator IFlipCard(Quaternion newRot)
    {
        float duration = 0.5f;
        while (duration > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * rotateLerp);
            yield return new WaitForEndOfFrame();
            duration -= Time.deltaTime;
        }
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
    
    string InsertSpaceBetweenWords(string input)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        
        foreach (char c in input) {
            // Check if the character is uppercase
            if (char.IsUpper(c)) {
                // Insert a space before the uppercase letter
                sb.Append(' ');
            }
            // Append the character to the result
            sb.Append(c);
        }
        
        return sb.ToString().Trim();
    }
}
