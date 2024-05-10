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
            var count = displays[laneIndex].transform.GetChild(1).GetComponent< TMP_Text>();
            count.text = wave.laneSpawnData[i].unitSpawnData[0].amount.ToString();
            var name = displays[laneIndex].transform.GetChild(2).GetComponent< TMP_Text>();
            name.text = wave.laneSpawnData[i].unitSpawnData[0].enemy.ToString();
            
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
