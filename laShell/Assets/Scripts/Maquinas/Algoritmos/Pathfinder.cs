using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    List<Node> openNodes = new List<Node>();
    List<Node> closedNodes = new List<Node>();
    List<Node> path = new List<Node>();
    Node selectedNode = null;
	
	void Update ()
    {
		
	}

    public void OpenNode(Node n, Node parent)
    {
        if (!n.GetOpen())
        {
            if (parent == null)
            {
                n.SetParent(null);
            }
            else
            {
                n.SetParent(parent);
            }
            n.SetOpen(true);
            openNodes.Add(n);
        }
    }
}
