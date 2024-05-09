using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChestUnit : MonoBehaviour
{
    [SerializeField] private CardUnit cardUnit;
    public GameObject gemPrefab;
    public float dropDown = 2;
    public float dropInterval = 10f;
    private float nextDropTime;

    public Vector3 spawnPosition;
    public float spawnForce;

    public float randomRadius = 1;

    private void OnEnable()
    {
        cardUnit.LevelUpEvent += LevelUp;
    }
    
    private void OnDisable()
    {
        cardUnit.LevelUpEvent -= LevelUp;
    }

    void LevelUp(int level)
    {
        dropInterval -= dropDown; // Lower the drop time
        nextDropTime -= dropDown; // Lower the current interval
    }

    void Start()
    {
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
