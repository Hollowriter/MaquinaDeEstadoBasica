using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    List<Node> obj = new List<Node>();
    // List<Node> path = new List<Node>(); // A pathfinder
    float cost = 1;
    float totalCost = 0;
    bool isOpen = false;
    bool isClosed = false;
    bool isDestiny = false;
    bool isPath = false;
    Node theParent = null;

    public void AddAdjacent(Node n)
    {
        if (!obj.Contains(n))
            obj.Add(n);
    }
	
	public List<Node> Adjacents()
	{
		return obj;
	}

   /* public void OpenNode(Node n, Node parent)
    {
        if(parent == null)
        {
            theParent = null;
        }

        if(n.GetOpen() == false && n.GetClosed() == false)
        {
            n.SetOpen(true);

            if(isDestiny)
            {
                obj.Clear();
                GetPath();
            }
            else
            {
                n.SetClosed(true);
                for(int i = 0; i < obj.Count; i++)
                {
                    OpenNode(obj[i], this);
                }
            }
        }
    }*/ // A pathfinder

   /* public void SelectNode()
    {

    }*/ // A pathfinder

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
        theParent = parent;
    }

    public void SetAsPath(bool sure) 
    {
        isPath = sure;
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

    public Node GetParent() 
    {
        return theParent;
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
