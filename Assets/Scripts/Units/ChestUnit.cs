using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUnit : MonoBehaviour
{
    public GameObject gemPrefab;
    
    public float dropInterval = 10f;
    private float nextDropTime;

    public Vector3 spawnPosition;
    public float spawnForce;

    public float randomRadius = 1;


     void Start()
    {

        // commented out for testing so the sphere's drop from the start
        // drop the first sphere after dropInterval time
        nextDropTime = Time.time + dropInterval;
    }

    void Update()
    {
        if (Time.time >= nextDropTime)
        {
            DropGem();
            nextDropTime = Time.time + dropInterval;
        }
    }

    void DropGem()
    {
        Vector3 spawnDirection = new Vector3(Random.Range(-randomRadius, randomRadius),1f, Random.Range(-randomRadius, randomRadius));
        Vector3 spawn_position = spawnPosition + transform.position;
        spawn_position += new Vector3(spawnDirection.x,0f, spawnDirection.z);

        // instantiate gem
        var go = Instantiate(gemPrefab, spawn_position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce(spawnForce * spawnDirection.normalized, ForceMode.Impulse);
    }
}
