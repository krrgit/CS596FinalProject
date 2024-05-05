using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// UI Card
public class UICard : MonoBehaviour, IComparable
{
    const int IGNORE_RAYCAST_LAYER =  2;

    [SerializeField] CardSO cardSO;
    [SerializeField] private bool cardHeld;
    [SerializeField] private float moveLerp = 3.0f;
    [SerializeField] private float rotateLerp = 10.0f;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector3 handPos;
    [SerializeField] private Vector3 displayPos = new Vector3(0.0f, 0.25f, 0.0f);

    [Header("UI Elements")] 
    [SerializeField] private TMP_Text tmpName;
    [SerializeField] private TMP_Text tmpCost;
    [SerializeField] private TMP_Text tmpAttack;
    [SerializeField] private TMP_Text tmpHealth;
    [SerializeField] private TMP_Text tmpDesc;
    
    
    private int cardLayer;

    public CardSO CardSO
    {
        get { return cardSO; }
    }

    public bool IsHeld
    {
        get { return cardHeld; }
    }

    public void SetCardClass(CardSO _cardSO)
    {
        cardSO = _cardSO;
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
        GameObject prefab = UnitSpawner.Instance.GetCardDisplay(cardSO.name);
        if (prefab)
        {
            var go = Instantiate(prefab, transform.position, transform.rotation);
            go.transform.parent = transform;
            go.transform.localPosition = displayPos;
        }
        
        tmpName.text = cardSO.cardName;
        tmpCost.text = cardSO.cost.ToString();
        tmpAttack.text = cardSO.attack.ToString();
        tmpHealth.text = cardSO.health.ToString();
        tmpDesc.text = cardSO.description;
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
}
