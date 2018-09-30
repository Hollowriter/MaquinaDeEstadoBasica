using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderDijkstra : MonoBehaviour 
{
    List<Node> openNodes = new List<Node>();
    List<Node> closedNodes = new List<Node>();
    List<Node> path = new List<Node>();
    Node selectedNode = null;

    public List<Node> GetPath(Node origin, Node destiny) 
    {
        path.Clear();
        destiny.SetDestiny(true);
        OpenNode(origin, null);
        while(openNodes.Count > 0) 
        {
            selectedNode = SelectNode();
            if(selectedNode.GetDestiny()) 
            {
                CallForParents(selectedNode);
                return path;
            }
            CloseNode(selectedNode);
            OpenAdjacents(selectedNode);
        }
        return path;
    }

    public void OpenNode(Node n, Node parent) 
    {
        if(!n.GetOpen() && !n.GetClosed()) 
        {
            if(parent != null) 
            {
                n.SetParent(parent);
            }
            n.SetOpen(true);
            openNodes.Add(n);
        }
    }

    public void CloseNode(Node n) 
    {
        if(!n.GetClosed() && n.GetOpen()) 
        {
            n.SetClosed(true);
            openNodes.Remove(n);
            closedNodes.Add(n);
        }
    }

    public void OpenAdjacents(Node n) 
    {
        for(int i = 0; i < n.Adjacents().Count; i++) 
        {
            OpenNode(n.Adjacents()[i], n);
        }
    }

    public Node SelectNode() 
    {
        int selectedNode = 0;
        float valueNode = openNodes[selectedNode].GetTotalCost();
        for(int i = 0; i < openNodes.Count; i++) 
        {
            if(openNodes[i].GetTotalCost() < valueNode) 
            {
                valueNode = openNodes[i].GetTotalCost();
                selectedNode = i;
            }
        }
        return openNodes[selectedNode];
    }

    public void CallForParents(Node n) 
    {
        if(n.GetParent()) 
        {
            n.GetParent().SetAsPath(true);
            CallForParents(n.GetParent());
            path.Add(n.GetParent());
        }
    }

    public bool DestinyNode() 
    {
        return selectedNode.GetDestiny();
    }
}
