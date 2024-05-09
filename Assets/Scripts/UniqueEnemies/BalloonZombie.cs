using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonZombie : MonoBehaviour
{
    public string playerTag = "Player";
    public string wallTag = "Wall";
    public float speed = 1.0f; // MUST BE THE SAME WITH ENEMYAI MOVESPEED

    private Transform target;

    void Update()
    {
        FindTarget();

        if (target != null)
        {
            // Move towards the target
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void FindTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            GameObject wall = GameObject.FindGameObjectWithTag(wallTag);
            if (wall != null)
            {
                target = wall.transform;
            }
        }
    }
}
