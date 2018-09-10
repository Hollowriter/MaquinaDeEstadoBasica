using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    List<Node> openNodes = new List<Node>();
    List<Node> closedNodes = new List<Node>();
    List<Node> path = new List<Node>();
    Node selectedNode = null;

	public List<Node> GetPath(Node origin)
	{
        path.Clear();
		OpenNode(origin, null);
		while (openNodes.Count > 0)
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
        if (!n.GetOpen() && !n.GetClosed())
        {
            if (parent != null)
            {
                n.SetParent(parent);
            }
            n.SetOpen(true);
            openNodes.Add(n);
        }
    }

    public void CloseNode(Node n)
    {
        if (!n.GetClosed() && n.GetOpen())
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
        return openNodes[0];
    }

    public void CallForParents(Node n) 
    {
        if(n.GetParent()) 
        {
            path.Add(n.GetParent());
            CallForParents(n.GetParent());
        }
    }

    public bool DestinyNode()
    {
        return selectedNode.GetDestiny();
    }
}
