using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Grid
{
    //Attributes of the grid
    int width;
    int height;
    public float cellSize;
    Vector3 originPosition;
    TextMesh[,] debugTextArray;
    int[,] gridArray;
    //Basic Constructor
    public Grid(int width, int height,float cellSize,Vector3 originPosition) 
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];
       

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                //debugTextArray[x,y]= UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y)+new Vector3(cellSize,cellSize)*0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        }
    }
    //Gets the world position of the gridArray(x,y).
    public Vector3 GetWorldPosition(int x, int y) 
    {
        return new Vector3(x, y) * cellSize+this.originPosition;
    }
    //Gets the worldPositions corresponding gridArray
    private void GetXY(Vector3 worldPosition, out int x, out int y) 
    {
        x = Mathf.FloorToInt((worldPosition-this.originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition-this.originPosition).y / cellSize);
    }
    //Setters method for gridArray values
    public void SetValue(int x, int y, int value) 
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            //debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
    }
    public void SetValue(Vector3 worldPosition, int value) 
    {
        int x, y;
        GetXY(worldPosition,out x, out y);
        SetValue(x, y, value);
    }
    //Getters for gridArray values
    public int GetValue(int x, int y) 
    {
        if (x >= 0 && y >= 0 && x < width && y < height) 
        {
            return gridArray[x, y];
        }
        else 
        {
            return 0;
        }
    }
    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);

    }
    public void SetMapValues() 
    {

    }
}
