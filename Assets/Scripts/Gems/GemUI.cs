using UnityEngine;
using TMPro;

public class GemUI : MonoBehaviour
{
    public TMP_Text gemText;
    public GemCollector gemCollector; 

    void Update()
    {
        // update gem count in UI
        gemText.text = "Gems Collected: " + gemCollector.GetGemCount().ToString();
    }
}
