using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorHealth : MonoBehaviour
{
    public int currentHealth;
    public int startHealth = 100;

    [SerializeField]
    private GameObject Shield;
    private Renderer shieldRenderer;
    private Color color;
    public Material damagedShield; // for DamagedShield
    public Material destroyedShield; // for DestroyedShield


    void Start()
    {
        currentHealth = startHealth;
        shieldRenderer = Shield.GetComponent<Renderer>();
        color = shieldRenderer.material.color;
    }

    // Update is called once per frame
    // void Update()
    // {
    //    
    // }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Took " + damage + " damage");

        if (currentHealth <= 100)
        {
            shieldRenderer.material = damagedShield;
        }

        if (currentHealth <= 0)
        {
            Dying();
        }
    }

    // Dying "Animation"
    private void Dying()
    {
        shieldRenderer.material = destroyedShield;

        // Freeze it

        Invoke("Death", 2f); // Dies after 2 seconds
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
