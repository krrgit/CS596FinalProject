using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridCell : MonoBehaviour
{
    //diff colors to make grid not look bland
    [SerializeField] private Color baseColor;
    [SerializeField] private Color offsetColor;
    [SerializeField] private Renderer renderer;
    public bool isOpen = true;
    private Color origColor;

    public bool isHighlighted;
    public CardUnit OccupyingUnit { get; private set; }
    private void Start()
    {
        origColor = renderer.material.color;
    }

    private void OnMouseDown()
    {
        //inputManager.CellClick(this);
        print("clicked cell at: " + transform.position);
    }


    public void Init(bool isOffset)
    {
        renderer.material.color = isOffset ? offsetColor : baseColor;
    }
    
    //highlights current cell that mouse is hovering
    private void OnMouseEnter()
    {
        Color newColor = new Color(1f, 1f, 1f, .01f);
        renderer.material.color = newColor;
        isHighlighted = true;
    }

    private void OnMouseExit()
    {
        renderer.material.color = origColor;
        isHighlighted = false;
    }


    public Vector3 GetCellPosition()
    {
        return transform.position;
    }

    public void SetOccupyingUnit(CardUnit unit)
    {
        OccupyingUnit = unit;
    }

    public void RemoveOccupyingUnit()
    {
        OccupyingUnit = null;
    }
    
    
}