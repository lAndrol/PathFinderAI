using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveController : MonoBehaviour
{
    Grid grid;
    int positionX, positionY;
    public Stack ShortestStack;
    GameObject[] PassedPlaces;
    [SerializeField]GameObject target;
    private void Start()
    {
        Invoke("GetGridFromWorldManager", 0.1f);
        Invoke("MarkNode",0.2f);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Invoke("TeleportToLocation",0.4f);
        }
        if (this.transform.position ==target.transform.position)
        {
            Debug.Log("Caugth");
        }
    }
    public void TeleportToLocation()
    {
        this.transform.position = FindWay().position;

    }
    public void MoveRight()
    {

    }
    public void MoveLeft()
    {

    }
    public void RightWallJump()
    {
        //if not on wall
        //if on wall
    }
    public void LeftWallJump()
    {
        //if not on wall
        //if on wall
    }
    public void LeftJump()
    {

    }
    public void RightJump()
    {

    }
    public void RightDash()
    {

    }
    public void LeftDash()
    {

    }
    public void LeftDropDown()
    {

    }
    public void RightDropDown()
    {

    }
    public void FarRightWallJump()
    {

    }
    public void FarLeftWallJump()
    {

    }
    public void letWallGo()
    {

    }
    void MarkNode()
    {
        grid.GetXY(this.transform.position, out positionX, out positionY);
        string temp = positionX.ToString() + "," + positionY.ToString();
        GameObject currentNode = GameObject.Find(temp);
        currentNode.GetComponent<Node>().TogglePlayerHere();
    }
    void DeMarkNode()
    {

    }
    void GetGridFromWorldManager()
    {
        grid = GameObject.FindGameObjectWithTag("WorldController").GetComponent<WorldManager>().getGrid();
    }
    Transform FindWay()
    {
        grid.GetXY(this.transform.position, out positionX, out positionY);
        string temp = positionX.ToString() + "," + positionY.ToString();
        GameObject currentNode = GameObject.Find(temp);
        Transform position = currentNode.GetComponent<Node>().GoToWay();
        return position;
    }
}
