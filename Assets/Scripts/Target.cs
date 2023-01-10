using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Target : MonoBehaviour
{
    int positionX, positionY;
    Grid grid;
    GameObject nodeMap;
    [SerializeField] GameObject placeHolder;
    bool whereToMoveIsPresented = false;
    bool gridPresent = false;
    private void Start()
    {
        nodeMap = GameObject.Find("NodeMap");
        Invoke("GetGridFromWorldManager", 0.1f);
    }
    private void Update()
    {
        if (gridPresent)
        {
            ShowWhereToMove();
        }
       
        if (Input.GetMouseButtonDown(0))
        {
            
            Invoke("SetDistance",0.2f);
            Invoke("FixSearched", 0.5f);
            Invoke("MoveToTarget", 0.6f);
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
        this.gridPresent = true;
    }
    void SetDistance()
    {
        grid.GetXY(this.transform.position, out positionX, out positionY);
        string temp = positionX.ToString() + "," + positionY.ToString();
        GameObject currentNode = GameObject.Find(temp);
        currentNode.GetComponent<Node>().SetDistanceToTarget(1); 
    }
    void FixSearched()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");

        for(int i = 0; i < nodes.Length; i++)
        {
            nodes[i].GetComponent<Node>().SetState();
        }
    }
    private void ShowWhereToMove()
    {
        if (!whereToMoveIsPresented)
        {
            grid.GetXY(this.transform.position, out positionX, out positionY);
            string temp = positionX.ToString() + "," + positionY.ToString();
            GameObject currentNode = GameObject.Find(temp);
            GameObject[] spawnTo = currentNode.GetComponent<Node>().GetLegalNodes();
            for (int i = 0; i < spawnTo.Length; i++)
            {
                if (spawnTo[i] != null)
                {
                    Instantiate(placeHolder, spawnTo[i].transform.position, spawnTo[i].transform.rotation);
                }
                
            }
            whereToMoveIsPresented = true;
        }
        
    }
    private void MoveToTarget()
    {
        int x, y;
        grid.GetXY(this.transform.position, out positionX, out positionY);
        string temp = positionX.ToString() + "," + positionY.ToString();
        GameObject currentNode = GameObject.Find(temp);
        GameObject[] spawnTo = currentNode.GetComponent<Node>().GetLegalNodes();
        grid.GetXY(UtilsClass.GetMouseWorldPosition(), out x, out y);
        temp = x.ToString() + "," + y.ToString();
        for (int i = 0; i < spawnTo.Length; i++)
        {
            if (spawnTo[i] != null)
            {
                if (temp == spawnTo[i].name)
                {
                    this.transform.position = spawnTo[i].transform.position;
                    whereToMoveIsPresented = false;
                    GameObject[] placeHolders = GameObject.FindGameObjectsWithTag("PlaceHolder");
                    foreach (GameObject item in placeHolders)
                    {
                        item.GetComponent<PlaceHolder>().DestroyObject();
                    }
                    GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
                    foreach (GameObject node in nodes)
                    {
                        node.GetComponent<Node>().justPassed = false;
                    }
                    break;
                }
            }
            
        }
        
        
        
    }
}
