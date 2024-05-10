using System;
using UnityEngine;


public class GemCollector : MonoBehaviour
{
    [SerializeField] private int gem_count = 0;
    [SerializeField] private int newDayGemsToAdd = 12;

    private WaveSpawner waveSpawner;
    public delegate void GemCollectedDelegate(int gem_count);
    public GemCollectedDelegate GemCollectedEvent;

    void Start()
    {
        GemCollectedEvent?.Invoke(gem_count);

        WaveSpawner.Instance.NewDayEvent += NewDayAddGems;
    }

    private void OnDisable()
    {
        WaveSpawner.Instance.NewDayEvent -= NewDayAddGems;
    }

    void NewDayAddGems(int currentDay, int totalDays)
    {
        gem_count += newDayGemsToAdd;
        GemCollectedEvent?.Invoke(gem_count);
    }

    void Update()
    {
        // check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // raycast from mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // check if a gem is clicked
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("Gem"))
                {
                    // Call the CollectGem function to collect the gem
                    CollectGem(hit.collider.gameObject);
                }
            }
        }
    }

    void CollectGem(GameObject gem)
    {
        Debug.Log("Gem collected!");
        gem_count++;
        
        GemCollectedEvent?.Invoke(gem_count);
        
        SoundManager.Instance.PlayClip("collectgem"); // play sound fx when collecting gems

        // destroy the collected gem
        Destroy(gem);
    }

    public int GetGemCount()
    {
        return gem_count;
    }

    public void DecrementGemCount(int value)
    {
        gem_count -= value;
        GemCollectedEvent?.Invoke(gem_count);
    }
}
