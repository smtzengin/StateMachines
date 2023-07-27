using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "CellularGridData", 
    menuName = "ScriptableObjects/CellularGridData")]
public class CellularAutomataSO : ScriptableObject
{
    public int[,] cellularAutomataGrid;
    public Color[] pixels;

    public void CreateGrid(Vector2Int gridSize)
    {
        cellularAutomataGrid = new int[gridSize.x, gridSize.y];
    }
    
    public void ModifyCell(Vector2Int coordinates, int health)
    {
        cellularAutomataGrid[coordinates.x, coordinates.y] = health;
    }
    
    public int ReturnCellValue(Vector2Int coordinates)
    {
        return cellularAutomataGrid[coordinates.x, coordinates.y];
    }

    public IEnumerable<Vector2Int> LoopEveryCell(int width, int height)
    {
        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                yield return new Vector2Int(x, y);
            }
        }
    }
}


//Vector2Int coordinatesInt = new Vector2Int((int)coordinates.x, (int)coordinates.y);
// Debug.Log("coordinates" + coordinates + " value "
// + cellularAutomataGrid[coordinates.x, coordinates.y]);
