using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDeck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawCardCheck();
    }

    void DrawCardCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // raycast from mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // check if a card is clicked
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    print("Draw Card");
                    DeckManager.Instance.DrawCard();
                }
            }
        }
    }
}
