using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    List<Node> obj = new List<Node>();
    // List<Node> path = new List<Node>(); // A pathfinder
    float cost = 1;
    float heuristic = 0;
    float startPointCost = 0;
    float goalPointCost = 0;
    float totalCost = 0;
    bool isOpen = false;
    bool isClosed = false;
    bool isDestiny = false;
    bool isPath = false;
    Node theParent = null;
    Node theChild = null;

    public void AddAdjacent(Node n)
    {
        if (!obj.Contains(n))
            obj.Add(n);
    }
	
	public List<Node> Adjacents()
	{
		return obj;
	}

    public void SetOpen(bool open)
    {
        isOpen = open;
    }

    public void SetClosed(bool closed)
    {
        isClosed = closed;
    }

    public void SetDestiny(bool destiny)
    {
        isDestiny = destiny;
    }

    public void SetParent(Node parent)
    {
        //startPointCost++;
        theParent = parent;
        SetTotalCost(theParent);
        SetStartTotalCost(theParent);
        theParent.SetChild(this);
        // startPointCost = theParent.GetStartTotalCost() + startPointCost;
    }

    public void SetChild(Node child)
    {
        theChild = child;
        SetGoalTotalCost(child);
    }

    public void SetAsPath(bool sure) 
    {
        isPath = sure;
    }

    public void SetTotalCost(Node parent)  
    {
        if (parent == null) 
        {
            totalCost = cost;
        }
        else 
        {
            totalCost = cost + parent.totalCost;
        }
    }

    public void SetHeuristicalTotalCost()
    {
        heuristic = goalPointCost + startPointCost;
    }

    public void SetGoalTotalCost(Node child)
    {
        if (child)
        {
            goalPointCost = 1 + child.GetGoalTotalCost();
        }
    }

    public void SetStartTotalCost(Node parent)
    {
        if (parent)
        {
            startPointCost = 1 + GetParent().GetStartTotalCost();
        }
    }

    public float GetHeuristicalTotalCost()
    {
        SetHeuristicalTotalCost();
        return heuristic;
    }

    public float GetGoalTotalCost()
    {
        return goalPointCost;
    }

    public float GetStartTotalCost()
    {
        return startPointCost;
    }

    public bool GetOpen()
    {
        return isOpen;
    }

    public bool GetClosed()
    {
        return isClosed;
    }

    public bool GetDestiny()
    {
        return isDestiny;
    }

    public bool GetAsPath() 
    {
        return isPath;
    }

    public float GetTotalCost() 
    {
        return totalCost;
    }

    public Node GetParent() 
    {
        // theParent.SetGoalTotalCost(GetGoalTotalCost() + 1);
        return theParent;
    }

    public Node GetChild()
    {
        return theChild;
    }

    /*public List<Node> GetPath()
    {
        return path;
    }*/ // A pathfinder

    private void OnDrawGizmos()
    {
        if(isPath == true) 
        {
            Gizmos.color = Color.blue;
        }
        Gizmos.DrawCube(this.transform.position, Vector3.one);
    }
}
