using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int numRows;
    public int numColumns;
    public gridCell gridCellPrefab;
    
    // 2D array to hold grid cells
    public gridCell[,] gridCells;

    void Start()
    {
        CreateGrid();
        print(numRows + " " + numColumns);
    }
    
    // Creates the grid of cells
    void CreateGrid()
    {
        gridCells = new gridCell[numRows, numColumns];

        for (int col = 0; col < numColumns; col++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Vector3 cellPosition = new Vector3(col, 0f, row);
                gridCell cell = Instantiate(gridCellPrefab, cellPosition, Quaternion.identity);
                cell.name = $"Cell {col} {row}";
                cell.transform.SetParent(transform);
                bool isOffset = (row % 2 == 0 && col % 2 != 0) || (row % 2 != 0 && col % 2 == 0);
                cell.Init(isOffset);
                gridCells[row, col] = cell;
            }
        }
    }
    
    
    //gets cell position for enemies to spawn at
    public Vector3 GetCellPosition()
    {   
        
        Vector3 cellPosition = new Vector3(Random.Range(0,(numColumns - 1)), 1f, 0);
        return cellPosition;
    }
    
    public void CheckUnitsOnGrid()
    {
        for (int col = 0; col < numColumns; col++)
        {
            for (int row = 0; row < numRows; row++)
            {
                gridCell cell = gridCells[row, col];

            }
        }
    }
    
    
}
