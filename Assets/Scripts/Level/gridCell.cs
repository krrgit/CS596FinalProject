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

    private void Start()
    {
        origColor = renderer.material.color;
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
    }

    private void OnMouseExit()
    {
        renderer.material.color = origColor;
    }
    
}