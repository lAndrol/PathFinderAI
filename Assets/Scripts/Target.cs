using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    int positionX, positionY;
    Grid grid;
    GameObject nodeMap;
    private void Start()
    {
        nodeMap = GameObject.Find("NodeMap");
        Invoke("GetGridFromWorldManager", 0.1f);
    }
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SetDistance();
        }
    }
    void findNode()
    {
        grid.GetXY(this.transform.position, out positionX, out positionY);
        string temp = positionX.ToString() + "," + positionY.ToString();
        GameObject currentNode = GameObject.Find(temp);
        Stack ShortestStack = currentNode.GetComponent<Node>().FindWay().top;
        while (ShortestStack!=null)
        {
            Debug.Log(ShortestStack.node.name);
            ShortestStack = ShortestStack.nextStack;
     
        }
    }
    void GetGridFromWorldManager()
    {
        grid = GameObject.FindGameObjectWithTag("WorldController").GetComponent<WorldManager>().getGrid();
    }
    void SetDistance()
    {
        grid.GetXY(this.transform.position, out positionX, out positionY);
        string temp = positionX.ToString() + "," + positionY.ToString();
        GameObject currentNode = GameObject.Find(temp);
        currentNode.GetComponent<Node>().SetDistanceToTarget(1); 
    }
}
