using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class WorldManager : MonoBehaviour
{
    [SerializeField] GameObject allowedMovesNode;
    Grid grid;
    [SerializeField] int gridWidth;
    [SerializeField] int gridHeight;
   


    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(gridWidth, gridHeight, 10f,new Vector3(0,0,0));
        CreateNodeMap(gridWidth,gridHeight, 10f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 56);
        }
    }
    //Creates nodes in the center of the grid cells
    private void CreateNodeMap(int gridwidth, int gridHeight,float cellSize) 
    {
        for (int x = 0; x < gridwidth; x++) 
        {
            for (int y = 0; y < gridHeight; y++) 
            {
                GameObject temp =Instantiate(allowedMovesNode,new Vector3((x*cellSize)+(cellSize*0.5f), (y * cellSize) + (cellSize * 0.5f),0),new Quaternion(0,0,0,0));
                temp.name = x.ToString() + "," + y.ToString();
            }
        }
    
    }
   
}