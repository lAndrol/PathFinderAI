using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("0-Right 1-left")]
    [Header("2-RightWallJump 3-leftWallJump")]
    [Header("4-LeftJump 5-rightJump")]
    [Header("6-RightDash 7-LeftDash")]
    [Header("8-LeftDropDown 9-RightDropDown")]
    [Header("10-FarRightWallJump 11-FarLeftWallJump")]
    [SerializeField] GameObject[] legalNodes;
    [SerializeField] GameObject player;
    bool isSearched = false;
    bool playerHere = false;
    Grid grid;
    Stack way = new Stack();
    Stack minStack=new Stack();
    Stack distanceToEndStack;
    public int minStackLenght;
    public bool playerFound = false;
    int minCount = 1;
    public int minDistance = 999;
    public int checkDistance = 99;
    enum State
    {
        Searched,
        BeingSearched,
        NotSearched
    }
    State state = State.NotSearched;
    private void Start()
    {
        Invoke("InitilizeGrid", 0.2f);
    }
    public Stack FindWay(GameObject mainNode,Stack stack)
    {
        if (!this.isSearched)
        {
            stack.Push(this.gameObject);
            this.isSearched = true;
            if (playerHere)
            {

                playerFound = true;
                minStack = stack;
                Stack testStack = minStack.top;
                while (testStack != null)
                {
                    Debug.Log(testStack.node.name);
                    testStack = testStack.nextStack;

                }
                stack.Pop();
                return minStack;
            }
            for (int i = 0; i < legalNodes.Length; i++)
            {
                if (legalNodes[i] != null && legalNodes[i] != mainNode)
                {
                    stack = legalNodes[i].GetComponent<Node>().FindWay(this.gameObject, stack);
                    if (stack.top != null)
                    {
                        int stackLenght = CountStack(stack.top);
                        if (stackLenght < minStackLenght)
                        {
                            minStack = stack;
                            playerFound = true;
                            minStackLenght = stackLenght;
                        }
                    }
                }
            }
            stack.Pop();
            if (playerFound)
            {
                return minStack;
            }
            else
            {
                return stack;
            }
        }
        else
        {
            if (this.playerFound)
            {
                    Stack tempStack;
                    if (minStack.top != null)
                    {
                        if (CountStack(stack.top) < minStack.top.DistanceToEnd(this.gameObject, out tempStack))
                        {
                            if (tempStack.top != null)
                            {
                                int tempCount = CountStack(tempStack.top);
                                for (int i = 0; i < tempCount; i++)
                                {
                                    Debug.Log("sdaf");
                                    stack.Push(tempStack.Pop().node);
                                }
                                minStack = stack;
                                Stack testStack = minStack.top;
                                while (testStack != null)
                                {
                                    testStack = testStack.nextStack;

                                }
                                return minStack;
                            }
                        }
                        else
                        {   
                            return minStack;
                        }
                    }
                return stack;
            }
            return stack;
        }
        
       
    }
     
    public Stack FindWay()
    {
        way.Push(this.gameObject);
        if (this.playerFound)
        {
            Stack tempStack;
            if (minStack.top != null)
            {
                if (CountStack(way.top) < minStack.top.DistanceToEnd(this.gameObject, out tempStack))
                {
                    if (tempStack.top != null)
                    {
                        int tempCount = CountStack(tempStack.top);
                        for (int i = 0; i < tempCount; i++)
                        {
                            way.Push(tempStack.Pop().node);
                        }
                        minStack = way;
                        return minStack;
                    }
                }
                else
                {
                    return minStack;
                }
            }

        }
        if (playerHere)
        {
            playerFound = true;
            minStack = way;
            return minStack;
        }
        for (int i = 0; i < legalNodes.Length; i++)
        {
            if (legalNodes[i] != null)
            {
                way = legalNodes[i].GetComponent<Node>().FindWay(this.gameObject, way);
                if (way.top != null)
                {
                    int stackLenght = CountStack(way.top);
                    if (stackLenght < CountStack(minStack))
                    {
                        minStack = way;
                        playerFound = true;
                        minStackLenght = stackLenght;
                    }
                }
            }
        }
        return minStack;
    }
   
    public GameObject[] GetLegalNodes()
    {
        return this.legalNodes;
    }
    void InitilizeGrid()
    {
        grid = GameObject.Find("WorldManager").GetComponent<WorldManager>().getGrid();
    }
    public void TogglePlayerHere()
    {
        this.playerHere = !this.playerHere;
    }
    public int CountStack(Stack countingStack)
    {
        int count = 1;
        if (countingStack.PeekNextStack() != null)
        {
           count += CountStack(countingStack.PeekNextStack());
        }
        return count;
    }
    public void SetDistanceToTarget(GameObject mainNode, int distance)
    {
        
        bool loop = true;
        if (this.state == State.BeingSearched)
        {
            return;
        }
        while (loop)
        {
            switch (state)
            {
                case State.NotSearched:
                    state = State.BeingSearched;
                    break;
                case State.BeingSearched:
                    if (distance < this.minDistance)
                    {
                        this.minDistance = distance;
                        Debug.Log(this.gameObject.name + " AND " + minDistance);
                    }
                    if (playerHere)
                    {
                        return;
                    }
                    for (int i = 0; i < legalNodes.Length; i++)
                    {
                        if (legalNodes[i] != null && legalNodes[i] != mainNode)
                        {
                            legalNodes[i].GetComponent<Node>().SetDistanceToTarget(this.gameObject, distance + 1);
                        }
                    }
                    state = State.Searched;
                    loop = false;
                    break;
                case State.Searched:
                    if (distance < this.minDistance)
                    {
                        this.minDistance = distance;
                        this.state = State.BeingSearched;
                        this.SetDistanceToTarget(mainNode, distance);
                    }
                    else
                    {
                        distance = this.minDistance;
                        this.state = State.BeingSearched;
                        this.SetDistanceToTarget(mainNode, distance);
                    }
                    return;
                    break;
            }
        }
    }
    public void SetDistanceToTarget(int distance)
    {
        minDistance = 90;
        bool loop = true;
        if (this.state == State.BeingSearched)
        {
            return;
        }
        while (loop)
        {
            switch (state)
            {

                case State.NotSearched:
                    state = State.BeingSearched;
                    break;
                case State.BeingSearched:
                    if (distance < this.minDistance)
                    {
                        this.minDistance = distance;
                    }
                    else
                    {
                        distance = this.minDistance;
                    }
                    if (playerHere)
                    {
                        return;
                    }
                    for (int i = 0; i < legalNodes.Length; i++)
                    {
                        if (legalNodes[i] != null)
                        {
                            legalNodes[i].GetComponent<Node>().SetDistanceToTarget(this.gameObject, distance + 1);
                        }
                    }
                    state = State.Searched;
                    loop = false;
                    break;
                case State.Searched:
                    if (distance < this.minDistance)
                    {
                        this.minDistance = distance;
                        this.state = State.BeingSearched;
                        this.SetDistanceToTarget(this.gameObject, distance);
                    }
                    break;
            }

        }

    }
    public void PrintWay()
    {
        int legalNodeDis=99;
        int index=0;
        GameObject newNode = this.gameObject;
        Debug.Log(minDistance + " " + this.gameObject.name);
        while (newNode.name != "5,7")
        {
            for (int i = 0; i < newNode.GetComponent<Node>().legalNodes.Length; i++)
            {
                if (newNode.GetComponent<Node>().legalNodes[i] != null)
                {
                    legalNodeDis = newNode.GetComponent<Node>().legalNodes[i].GetComponent<Node>().minDistance;
                    Debug.Log(newNode.name+" BRAKE "+newNode.GetComponent<Node>().legalNodes[i].GetComponent<Node>().minDistance);
                    if (legalNodeDis < newNode.GetComponent<Node>().legalNodes[i].GetComponent<Node>().checkDistance)
                    {
                        index = i;
                        newNode.GetComponent<Node>().legalNodes[i].GetComponent<Node>().checkDistance = legalNodeDis;
                    }
                }
                
            }
            if (legalNodeDis <= 1)
            {
                return;
            }
            Debug.Log(newNode.GetComponent<Node>().legalNodes[index].name);
            newNode = newNode.GetComponent<Node>().legalNodes[index];
        }
    }
    
}
    