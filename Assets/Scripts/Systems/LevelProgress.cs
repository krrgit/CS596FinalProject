using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Level is finished when:
// 1. No more enemies to spawn.
// 2. No more enemies in enemy pool.
// Note: Enemy spawner NEEDS to call LevelProgress.Instance.SetFinishedSpawn() when its done spawning.
public class LevelProgress : MonoBehaviour
{
    public bool levelFinished = false;
    [SerializeField] private bool finishedSpawn = false;
    [SerializeField] private Transform enemyPool;        // Contains all the enemy objects. 
        
    // Notifies subscribers(other scripts) when the level finishes.
    delegate void FinishedLevelDelegate();
    FinishedLevelDelegate finishedLevelDelegate;

    public static LevelProgress Instance;

    // Called by spawner to let LevelProgress its done spawning.
    void SetFinishedSpawn()
    {
        finishedSpawn = true;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    void Update()
    {
        LevelEndCheck();
    }

    void LevelEndCheck()
    {
        if (levelFinished) return; // Stop checking when level is finished.
        
        if (!enemyPool)
        {
            print("LevelProgress: Enemy Pool must be set! Level Finished.");
            levelFinished = true;
        }

        if (enemyPool.childCount == 0 && finishedSpawn)
        {
            levelFinished = true;
            finishedLevelDelegate?.Invoke();
            print("LevelProgress: Level Finished.");
        }
    }
}
