using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectManager : MonoBehaviour
{
    [SerializeField] GameObject hitPrefab;
    [SerializeField] string hitTag;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(hitTag))
        {
            Instantiate(hitPrefab, collision.contacts[0].point, Quaternion.identity);
            Debug.Log("Activate hit effect");
        }
    }


}