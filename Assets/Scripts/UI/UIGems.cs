using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGems : MonoBehaviour
{
    [SerializeField] private GemCollector gemCollector;
    [SerializeField] private TMP_Text gemCountText;

    private void OnEnable()
    {
        gemCollector.GemCollectedEvent += UpdateText;
    }
    
    private void OnDisable()
    {
        gemCollector.GemCollectedEvent -= UpdateText;
    }

    void UpdateText(int gem_count)
    {
        gemCountText.text = gem_count.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
