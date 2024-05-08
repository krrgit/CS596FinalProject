using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public GameObject gemPrefab;
    
    public float dropInterval = 10f;
    private float nextDropTime;

    public Vector2 minSpawnPosition = new Vector2(0f, 0f);
    public Vector2 maxSpawnPosition = new Vector2(4f, 7f);

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
        // random x position within range of screen width
        float randomX = Random.Range(minSpawnPosition.x, maxSpawnPosition.x);
        float randomZ = Random.Range(minSpawnPosition.y, maxSpawnPosition.y);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, randomZ);

        // maintain z position of gem
        // worldPosition.z = transform.position.z;
        
        // instantiate gem
        Instantiate(gemPrefab, spawnPosition, Quaternion.identity);
    }

}
