using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize = 10;
    public GridManager gridManager;
    public int enemiesPerWave = 2;
    public int waveOfEnemies = 1;
    
    public float spawnTimerBetweenWaves = 3.0f;
    public float spawnTimerBetweenEnemies = 1.0f;
    [SerializeField] bool isSpawning = false;

    private LevelProgress levelProgress;
    private void Start()
    {
        levelProgress = FindObjectOfType<LevelProgress>();
        StartCoroutine(SpawnEnemiesSlowly());
    }

    private void Update()
    {
        if (isSpawning)
        {
            StartCoroutine(SpawnEnemiesSlowly());
            isSpawning = false;
        }
    }

    GameObject InstantiateEnemy(Vector3 spawnPosition)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        newEnemy.transform.SetParent(transform);
        return newEnemy;
    }

    private IEnumerator SpawnEnemiesSlowly()
    {
        for (int j = 0; j < waveOfEnemies; j++)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                if (isSpawning)
                {
                    Vector3 spawnPosition = gridManager.GetCellPosition();
                    InstantiateEnemy(spawnPosition);
                    yield return new WaitForSeconds(spawnTimerBetweenEnemies);
                }
            }
            yield return new WaitForSeconds(spawnTimerBetweenWaves);
        }
        //Level finished when all enemies are dead
        LevelProgress.Instance.SetFinishedSpawn();
    }
}