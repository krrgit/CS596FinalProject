using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWaveDisplay : MonoBehaviour
{
    [SerializeField] private GameObject infoPrefab;
    [SerializeField] private GameObject[] displays;

    public void UpdateDisplays(WaveSpawnData wave)
    {
        HideAllDisplays();
        for(int i=0;i<wave.laneSpawnData.Length;++i)
        {
            int laneIndex = wave.laneSpawnData[i].laneIndex;
            var text = displays[laneIndex].transform.GetChild(1).GetComponent< TMP_Text>();
            text.text = wave.laneSpawnData[i].unitSpawnData[0].amount.ToString();
            displays[laneIndex].SetActive(true);
        }
    }

    public void HideAllDisplays()
    {
        for (int i = 0; i < displays.Length; ++i)
        {
            displays[i].SetActive(false);
        }
    }
}
