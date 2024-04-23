using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputManager : MonoBehaviour
{
    private DeckManager deckManager;

    void Start()
    {
        deckManager = DeckManager.Instance;
    }

    public void CellClick(gridCell cell)
    {
        if (cell.isHighlighted)
        {
            Vector3 cellPosition = cell.GetCellPosition();
            CardClass playedCard = deckManager.PlayCard();
            cellPosition.y = 1.0f;
            if (playedCard != null)
            {
                Instantiate(playedCard.prefab, cellPosition, Quaternion.identity);
                print("created slime at: " + cellPosition);
                deckManager.ReturnFieldCardToDeck(playedCard);
            }
            /*else
            {
                print("playedCard is null!");
            }*/
        }
    }
}
