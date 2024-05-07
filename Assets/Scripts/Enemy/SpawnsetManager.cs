using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Enemies
{
    PrayStatue,
    EyeRing,
    Mirror,
    BossBaby,
    EnemyCount
}

[System.Serializable]
public struct SpawnData
{
    public Enemies enemy;
    public int spawnTime;
}
public class SpawnsetManager : MonoBehaviour
{
    [SerializeField] private SpawnData[] spawnset;
    [SerializeField] private int index = 0;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject spawnEffect;
    [SerializeField] private float spawnEffectDelay = 0.33f;
    
    private bool finishedSpawn;
    private bool waveCleared;
 
    
    // Start is called before the first frame update
    void Start()
    {
        if (spawnset.Length == 0) return;
        
        StartCoroutine(ISpawn());
    }

    private void LateUpdate()
    {
        ClearedCheck();
    }

    void ClearedCheck()
    {
        if (!finishedSpawn || waveCleared) return;

        if (transform.childCount <= 0)
        {
            waveCleared = true;
            //StageEndAnimation.Instance.StartAnimation();
        }
    }

    IEnumerator ISpawn()
    {
        yield return new WaitForSeconds(spawnset[index].spawnTime);
        
        while (!finishedSpawn)
        {
            var spawnPoint = GetSpawnPoint();
            SpawnEffect(spawnPoint);

            yield return new WaitForSeconds(spawnEffectDelay);
            
            // Spawn Enemy
            Spawn(enemyPrefabs[(int)spawnset[index].enemy], spawnPoint);
            index++;
            
            if (index < spawnset.Length)
            {
                yield return new WaitForSeconds(spawnset[index].spawnTime - spawnset[index-1].spawnTime - spawnEffectDelay);
            } else {
                finishedSpawn = true;
            }
        }
    }

    Vector3 GetSpawnPoint()
    {
        // Set Spawn point to edge of radius from spawner
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        return dir.normalized;
    }

    void SpawnEffect(Vector3 spawnPoint)
    {
        if (!spawnEffect) return;
        Instantiate(spawnEffect, transform.position + spawnPoint, Quaternion.identity);
    }
    
    void Spawn(GameObject enemy, Vector3 spawnPoint)
    {
        var go = Instantiate(enemy, transform.position + spawnPoint, Quaternion.identity);
        go.transform.parent = transform;
        // print("Spawn" + enemySpawn.name);
    }
}
