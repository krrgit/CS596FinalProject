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
    private Color origColor;

    [SerializeField] private MouseInputManager inputManager;

    public bool isHighlighted;

    private void Start()
    {
        origColor = renderer.material.color;
        inputManager = FindObjectOfType<MouseInputManager>();
    }

    private void OnMouseDown()
    {
        inputManager.CellClick(this);
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
}