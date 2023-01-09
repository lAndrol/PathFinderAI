using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack
{
    public GameObject node;
    public Stack nextStack, top,tail;
    public Stack()
    {
        top = null;
        nextStack = null;
        node = null;
        tail = null;
    }
    public Stack(GameObject newNode)
    {
        this.node = newNode;
        this.nextStack = null;
    }
    public void Push(GameObject newNode)
    {
        Stack newStack;
        if (this.top == null)
        {
            newStack = new Stack(newNode);
            this.top = newStack;
            this.tail = newStack;
            return;
        }
        newStack = new Stack(newNode);
        newStack.nextStack = this.top;
        newStack.tail = this.tail;
        this.top = newStack;
    }
    public Stack Pop()
    {
        Stack temp = this.top;
        if (temp != null)
        {
            if (temp.nextStack == null)
            {
                temp = this.top;
                this.top = null;
                temp.nextStack = null;
                return temp;
            }
            temp = this.top;
            this.top = this.top.nextStack;
            temp.nextStack = null;
            return temp;

        }
        else
        {
            return new Stack();
        }

    }
    
    public Stack PeekNextStack()
    {
        return nextStack;
    }
    public Stack Peek()
    {
        return top;
    }
    public int DistanceToEnd(GameObject node,out Stack returnStack)
    {
        int count = 0;
        returnStack = new Stack();
        if (this.nextStack != null)
        {
            Stack tempStack = this.nextStack;
            if (tempStack != null)
            {
                while (tempStack.node.name != node.name)
                {
                    returnStack.Push(tempStack.node);
                    Debug.Log(returnStack.top.node);
                    if (tempStack.PeekNextStack() != null)
                    {
                        tempStack = tempStack.PeekNextStack();
                    }
                    else
                    {
                        return -1;
                    }
                }
                while (tempStack.PeekNextStack() != null)
                {
                    count++;
                    tempStack = tempStack.PeekNextStack();
                }
                Debug.Log("Finished this node");
                return count;
            }
            
        }
        return -1;
    }
    public void SetAfterTail(Stack add)
    {
        this.tail.nextStack = add;
    }
    public GameObject getNode()
    {
        return this.node;
    }
}
