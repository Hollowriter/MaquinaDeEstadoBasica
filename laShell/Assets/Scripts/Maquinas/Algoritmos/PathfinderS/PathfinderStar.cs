using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderStar : MonoBehaviour
{
    List<Node> openNodes = new List<Node>();
    List<Node> closedNodes = new List<Node>();
    List<Node> path = new List<Node>();
    Node selectedNode = null;
    Node selectedDestiny = null;

    public List<Node> GetPath(Node origin, Node destiny)
    {
        /*if (selectedDestiny == null)
        {*/
            ClearAnything();
        // }
        /*else
        {
            ClearAnythingWithNode(selectedDestiny);
        }*/
        selectedDestiny = destiny;
        destiny.SetDestiny(true);
        OpenNode(origin, null);
        while (openNodes.Count > 0)
        {
            selectedNode = SelectNode();
            if (selectedNode.GetDestiny())
            {
                CallForParents(selectedNode);
                return path;
            }
            CloseNode(selectedNode);
            OpenAdjacents(selectedNode);
        }
        return path;
    }

    public void ClearAnything()
    {
        if (selectedDestiny != null)
        {
            selectedDestiny.SetDestiny(false);
            selectedDestiny = null;
        }
        if (path.Count > 0)
        {
            for (int i = 0; i < path.Count; i++)
            {
                path[i].SetAsPath(false);
                path[i].SetOpen(false);
                path[i].SetClosed(false);
                if (path[i].GetChild() != null)
                {
                    path[i].SetChild(null);
                }
                if (path[i].GetParent() != null)
                {
                    path[i].SetParent(null);
                }
            }
        }
        if (openNodes.Count > 0)
        {
            for (int i = 0; i < openNodes.Count; i++)
            {
                openNodes[i].SetOpen(false);
            }
        }
        if (closedNodes.Count > 0)
        {
            for (int i = 0; i < closedNodes.Count; i++)
            {
                closedNodes[i].SetClosed(false);
            }
        }
        path.Clear();
        openNodes.Clear();
        closedNodes.Clear();
        selectedNode = null;
    }

    /*public void ClearAnythingWithNode(Node destiny)
    {
        destiny.SetDestiny(false);
        path.Clear();
        openNodes.Clear();
        closedNodes.Clear();
        selectedNode = null;
    }*/

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
        for (int i = 0; i < n.Adjacents().Count; i++)
        {
            OpenNode(n.Adjacents()[i], n);
        }
    }

    public Node SelectNode()
    {
        int selectedNode = 0;
        float valueNode = openNodes[selectedNode].GetTotalCost() + openNodes[selectedNode].GetHeuristicalTotalCost();
        for (int i = 0; i < openNodes.Count; i++)
        {
            if (openNodes[i].GetTotalCost() + openNodes[i].GetHeuristicalTotalCost() < valueNode)
            {
                valueNode = openNodes[i].GetTotalCost() + openNodes[i].GetHeuristicalTotalCost();
                selectedNode = i;
            }
        }
        return openNodes[selectedNode];
    }

    public void CallForParents(Node n)
    {
        if (n.GetParent())
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
