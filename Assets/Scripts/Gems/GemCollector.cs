using UnityEngine;


public class GemCollector : MonoBehaviour
{
    [SerializeField]private int gem_count = 0;

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
        
        // destroy the collected gem
        Destroy(gem);
    }

    public int GetGemCount()
    {
        return gem_count;
    }
}
