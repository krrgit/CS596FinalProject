using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBuff : MonoBehaviour
{
    public float speed = 2f; // Speed

    private GameObject tractor; // Reference to the player object

    // Start is called before the first frame update
    void Start()
    {
        tractor = GameObject.FindGameObjectWithTag("Tractor"); // Find the player object
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tractorPosition = tractor.transform.position;

        // Calculate the distance between the player and the flying zombie
        float distanceToTractor = Vector3.Distance(transform.position, tractorPosition);

        // If Tractor is close enough, go up
        if (distanceToTractor < 4f)
        {
            speed = 6f;
        }
    }
}
