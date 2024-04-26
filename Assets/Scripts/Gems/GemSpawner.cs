using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public GameObject gemPrefab;
    
    public float dropInterval = 10f;
    private float nextDropTime;

     void Start()
    {
        // commented out for testing so the sphere's drop from the start
        // drop the first sphere after dropInterval time
        // nextDropTime = Time.time + dropInterval;
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
        float randomX = Random.Range(0f, Screen.width);
        
        // screen position to world position
        Vector3 screenPosition = new Vector3(randomX, Screen.height, 10f);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        
        // maintain z position of gem
        worldPosition.z = transform.position.z;
        
        // instantiate gem
        Instantiate(gemPrefab, worldPosition, Quaternion.identity);
    }

}
