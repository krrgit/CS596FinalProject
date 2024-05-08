using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class QueenSlime : MonoBehaviour
{
    
    public Vector3 origPos;

    public Vector3 inFrontCell;
    public Vector3 inBackCell;
    public GridManager gridManager;
    public int damageBonus = 20;
    public float atkSpeedMultiplier = 0.2f;
    public float newTimebetweenShots = 0.01f;
    private bool isLvl2 = false;
    private bool isLvl3 = false;
    void Start()
    {
        origPos = transform.position;
        Vector3 frontoffset = new Vector3(0, 0, -1);
        Vector3 backoffset = new Vector3(0, 0, 1);
        
        //get cell position to buff
        inFrontCell = origPos + frontoffset;
        inBackCell = origPos + backoffset;
        
        gridManager = FindObjectOfType<GridManager>();

        CardUnit cardUnit = GetComponent<CardUnit>();
        if (cardUnit != null)
        {
            print("detected card unit");
        }
        cardUnit.LevelUpEvent += OnCardUnitLevelUp;
    }

    private void Update()
    {
        gridManager.CheckUnitsOnGrid();

        BuffFrontCell();
        BuffBackCell();
        BuffEntireRow(gridManager.numRows);

    }
    private void OnCardUnitLevelUp(int newLevel)
    {

        if (newLevel == 2)
        {
            isLvl2 = true;
        }
        else if (newLevel == 3)
        {
            isLvl3 = true;
            isLvl2 = false;
        }
    }
    void BuffFrontCell()
    {
        //get front cell pos
        int row = Mathf.RoundToInt(inFrontCell.z);
        int col = Mathf.RoundToInt(inFrontCell.x);


        if (row >= 0 && row < gridManager.numRows && col >= 0 && col < gridManager.numColumns)
        {
            gridCell cell = gridManager.gridCells[row, col];

            if (cell != null && cell.OccupyingUnit != null)
            {
                if ((cell.OccupyingUnit.cardSO.cardName != "Big Cat Slime") ||
                    (cell.OccupyingUnit.cardSO.cardName != "Bunny Slime"))
                {
                    ApplyBuff(cell);
                }
            }
        }
    
    }
    void BuffBackCell()
    {
        if (isLvl2)
        {
            //get front cell pos
            int row = Mathf.RoundToInt(inBackCell.z);
            int col = Mathf.RoundToInt(inBackCell.x);


            if (row >= 0 && row < gridManager.numRows && col >= 0 && col < gridManager.numColumns)
            {
                gridCell cell = gridManager.gridCells[row, col];

                if (cell != null && cell.OccupyingUnit != null)
                {
                    if (cell.OccupyingUnit.cardSO.cardName != "Big Cat Slime" ||
                        cell.OccupyingUnit.cardSO.cardName != "Bunny Slime")
                    {
                        ApplyBuff(cell);
                    }
                }
            }
        }
    }

    void CheckCell(int row, int col)
    {

        if (row >= 0 && row < gridManager.numRows && col >= 0 && col < gridManager.numColumns)
        {
            gridCell cell = gridManager.gridCells[row, col];
            print(cell.transform.position);
            if (cell != null && cell.OccupyingUnit != null )
            {
                if (cell.OccupyingUnit.cardSO.cardName != "Big Cat Slime" ||
                    cell.OccupyingUnit.cardSO.cardName != "Bunny Slime")
                {
                    ApplyBuff(cell);
                }
            }
        }
    }
    void BuffEntireRow(int targetRow)
    {
        if (isLvl3)
        {
            int col = Mathf.RoundToInt(origPos.x);
            for (int row = 0; row < targetRow; row++)
            {
                CheckCell(row, col);
                Debug.Log($"Buffed cell at Column: {col}, Row: {row}");
            }
        }
    }
    void ApplyBuff(gridCell cell)
    {
        slimeShoot shoot = cell.OccupyingUnit.GetComponent<slimeShoot>();
        if (shoot != null)
        {
            shoot.canBuff = true;
            shoot.damageBonus = damageBonus;
            shoot.atkSpeedMultiplier = atkSpeedMultiplier;
            shoot.timeBetweenShots = newTimebetweenShots;
        }
    }
    

}