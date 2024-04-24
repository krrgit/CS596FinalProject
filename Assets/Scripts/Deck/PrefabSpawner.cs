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
    
    //list to map card types to their associated prefab
    public List<cardPrefab> cardPrefabs;

    public GameObject GetPrefab(Cards cardType)
    {
        //iterate through prefabs in list
        foreach (var pair in cardPrefabs)
        {
            //check if the card type matches the current cardPrefabs cardType and return it
            if (pair.cardType == cardType)
            {
                return pair.prefab;
            }
        }

        return null;
    }
}
