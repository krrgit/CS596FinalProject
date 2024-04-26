using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct cardPrefab
    {
        public Cards cardType;      //type of card
        public GameObject prefab;   //gameobject associated with the card
    }

    public GameObject[] prefabs;
    
    //list to map card types to their associated prefab
    public List<cardPrefab> cardPrefabs;

    public GameObject GetPrefab(Cards cardType)
    {
        return prefabs[(int)cardType];;
    }
}
