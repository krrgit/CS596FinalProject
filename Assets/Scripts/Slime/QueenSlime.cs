using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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

    private CardUnit cardUnit;
    private bool isLvl1 = false;
    private bool isLvl2 = false;
    void Start()
    {
        origPos = transform.position;
        Vector3 frontoffset = new Vector3(0, 0, -1);
        Vector3 backoffset = new Vector3(0, 0, 1);
        
        //get cell position to buff
        inFrontCell = origPos + frontoffset;
        inBackCell = origPos + backoffset;
        
        gridManager = FindObjectOfType<GridManager>();
        
        
        //subscribe to levelupevents
        cardUnit = GetComponent<CardUnit>();
        cardUnit.LevelUpEvent += OnCardUnitLevelUp;
    }

    private void Update()
    {
        gridManager.CheckUnitsOnGrid();
        if (isLvl1)
        {
            CheckInFrontCell();
        }
        else if (isLvl2)
        {
            CheckInFrontCell();
            CheckInBackCell();
        }
    }

    void CheckInFrontCell()
    {
        print("BUFFING FRONT");
        //get front cell pos
        int row = Mathf.RoundToInt(inFrontCell.z);
        int col = Mathf.RoundToInt(inFrontCell.x);
        
        
        if (row >= 0 && row < gridManager.numRows && col >= 0 && col < gridManager.numColumns)
        {
            gridCell cell = gridManager.gridCells[row, col];
            
            if (cell != null && cell.OccupyingUnit != null)
            {
                if (cell.OccupyingUnit.cardSO.cardName == "Sapling Slime" || cell.OccupyingUnit.cardSO.cardName == "Plant Slime")
                {
                    slimeShoot shoot = cell.OccupyingUnit.GetComponent<slimeShoot>();
                    shoot.canBuff = true;
                    shoot.damageBonus = damageBonus;
                    shoot.atkSpeedMultiplier = atkSpeedMultiplier;
                    shoot.timeBetweenShots = newTimebetweenShots;
                }
            }
        }
    }
    void CheckInBackCell()
    {
        print("BUFFING BACK");
        //get front cell pos
        int row = Mathf.RoundToInt(inBackCell.z);
        int col = Mathf.RoundToInt(inBackCell.x);
        
        
        if (row >= 0 && row < gridManager.numRows && col >= 0 && col < gridManager.numColumns)
        {
            gridCell cell = gridManager.gridCells[row, col];
            
            if (cell != null && cell.OccupyingUnit != null)
            {
                if (cell.OccupyingUnit.cardSO.cardName == "Sapling Slime" || cell.OccupyingUnit.cardSO.cardName == "Plant Slime")
                {
                    slimeShoot shoot = cell.OccupyingUnit.GetComponent<slimeShoot>();
                    shoot.canBuff = true;
                    shoot.damageBonus = damageBonus;
                    shoot.atkSpeedMultiplier = atkSpeedMultiplier;
                    shoot.timeBetweenShots = newTimebetweenShots;
                }
            }
        }
    }

    private void OnCardUnitLevelUp(int newLevel)
    {

        if (newLevel == 1)
        {
            isLvl1 = true;
            isLvl2 = false;
        }
        else if (newLevel == 2)
        {
            isLvl1 = true;
            isLvl2 = true;
        }
    }

}
