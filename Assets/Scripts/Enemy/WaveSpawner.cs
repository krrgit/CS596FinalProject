using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public enum EnemyClass
{
    Enemy01,
    Enemy02,
    Enemy03,
    Enemy04
};

[System.Serializable]
public struct UnitSpawnData
{
    public EnemyClass enemy;
    public int amount;
    public float spawnDelay;
    public float spawnInterval;
}

[System.Serializable]
public struct LaneSpawnData
{
    public int laneIndex;
    public UnitSpawnData[] unitSpawnData;
};

[System.Serializable]
public struct WaveSpawnData
{
    public LaneSpawnData[] laneSpawnData;
};

public enum TimeState
{
    Normal,
    FastForward,
    Paused
};

public class WaveSpawner : MonoBehaviour
{

    [SerializeField] private WaveSpawnData[] waveData;
    [SerializeField] private int currentWave;
    [SerializeField] private int totalDays = 3;
    [SerializeField] private int currentDay = 0;
    [SerializeField] private float halfCycleLength = 7;
    [SerializeField] float halfCycleIncrease = 5;
    [SerializeField] private float cycleTime;
    [SerializeField] private float cycleLength;
    [SerializeField] private TimeState timeState;
    [SerializeField] private bool isSpawning;
    [SerializeField] private bool finishedLastSpawn;
    [SerializeField] private bool finishedLevel;
    [SerializeField] private List<int> availableRows = new List<int>();
    [Header("References")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private UIWaveDisplay uiWaveDisplay;
    
    public delegate void NewDayDelegate(int currentDay, int totalDays);
    public NewDayDelegate NewDayEvent;
    

    // +2 unit every wave
    // start with 10
    // +7 on hard mode
    // new unit spawns start at 10
    // new unit day 4?
    // day 4, +9 on old unit
    // day 6, +5
    // day 7, +2
    // day 8, +1
    // boats (tougher units) +1
    
    // small units +: 2,2,2,9,5,2,1,2
    // big units +:-,-,-,10,1,0,1,0,1
    // new unit @ 9 w 24

    public float DayPercent
    {
        get { return cycleTime / cycleLength; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentDay = 1;
        totalDays = waveData.Length+1;
        NewDayEvent?.Invoke(currentDay, totalDays);
        cycleLength = 2 * halfCycleLength;
        cycleTime = 0;
        SetAllWaveLanes();
        uiWaveDisplay.UpdateDisplays(waveData[currentWave]);
    }

    private void Update()
    {
        if (finishedLevel) return;
        SpawnCheck();
        NewDayCheck();
        IncrementTime();

        FinishLevelCheck();
    }
    

    void ResetCycle()
    {
        halfCycleLength += halfCycleIncrease;
        cycleLength = halfCycleLength * 2;
        
        cycleTime = 0;
        isSpawning = false;
        currentDay++;
        NewDayEvent?.Invoke(currentDay, totalDays);
        
        timeState = TimeState.Normal;
        Time.timeScale = 1;

        if (currentWave < waveData.Length)
        {
            uiWaveDisplay.UpdateDisplays(waveData[currentWave]);
        }

        FinishLevelCheck();
        
    }

    void IncrementTime()
    {
        float deltaTime = Time.deltaTime;
        deltaTime *= timeState == TimeState.FastForward ? 2 : 1;
        deltaTime *= timeState == TimeState.Paused ? 0.01f : 1;
        cycleTime += deltaTime;
    }
    void NewDayCheck()
    {
        if (cycleTime >= cycleLength)
        {
            print("New Day");
            ResetCycle();
        }
    }

    void FinishLevelCheck()
    {
        if (finishedLastSpawn && transform.childCount == 0)
        {
            finishedLevel = true;
            LevelManager.Instance.SetFinishedSpawn();
        }
    }

    void SpawnCheck()
    {
        if (isSpawning) return;

        if (cycleTime >= halfCycleLength)
        {
            isSpawning = true;
            timeState = TimeState.Normal;
            Time.timeScale = 1;
            uiWaveDisplay.HideAllDisplays();
            if (currentWave < waveData.Length)
            {
                SpawnWave(waveData[currentWave]);
            }
        }
    }

    void SetAllWaveLanes()
    {
        for (int w = 0; w < waveData.Length; ++w)
        {
            ResetOpenLanes();
            for (int l = 0; l < waveData[w].laneSpawnData.Length; ++l) {
                waveData[w].laneSpawnData[l].laneIndex = GetRandomOpenLane();
            }
        }
    }

    void SpawnWave(WaveSpawnData wave)
    {
        print("Start Spawn");
        for (int i = 0; i < wave.laneSpawnData.Length; i++)
        {
            StartCoroutine(SpawnLane(wave.laneSpawnData[i]));
        }
        currentWave++;
    }

    IEnumerator SpawnLane(LaneSpawnData lane)
    {
        print("Use Lane " + lane.laneIndex);
        // Go through each unit spawn data
        for (int i = 0; i < lane.unitSpawnData.Length; i++)
        {
            if (lane.unitSpawnData[i].spawnInterval == 0) continue;
 
            // Delay before spawning this set of units 
            yield return new WaitForSeconds(lane.unitSpawnData[i].spawnDelay);

            // Spawn each unit for a given unit spawn data
            int spawned = 0;
            while (spawned < lane.unitSpawnData[i].amount)
            {
                SpawnUnit(lane.laneIndex, lane.unitSpawnData[i].enemy);
                spawned++;
                yield return new WaitForSeconds(lane.unitSpawnData[i].spawnInterval);
            }
        }
        
        if (currentDay >= totalDays - 1)
        {
            finishedLastSpawn = true;
        }
        
    }

    private void ResetOpenLanes()
    {
        print("Reset Open Lanes");
        availableRows.Clear();
        int maxRows = 5;
        int i = maxRows-1;
        while (i >= 0)
        {
            if (Random.Range(0, 2) == 0)
            {
                availableRows.Add(i);
            }
            else
            {
                availableRows.Insert(0,i);
            }
            i--;
        }
    }

    int GetRandomOpenLane()
    {
        int result = availableRows[0];
        availableRows.Remove(result);
        return result;
    }
    
    void SpawnUnit(int lane, EnemyClass enemyClass)
    {
        Vector3 position = gridManager.GetEnemySpawnPosition(lane) + transform.position;
        var go = Instantiate(enemyPrefabs[(int)enemyClass], position, Quaternion.identity);
        go.transform.parent = transform;
    }
    
    public void Pause()
    {
        timeState = timeState != TimeState.Paused ? TimeState.Paused : TimeState.Normal;
        Time.timeScale = timeState == TimeState.Paused ? 0.001f : 1;
    }

    public void FastFoward()
    {
        timeState = timeState != TimeState.FastForward ? TimeState.FastForward : TimeState.Normal;
        Time.timeScale = timeState == TimeState.FastForward ? 2 : 1;
    }
    
    
    
}
