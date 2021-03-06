﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderDepth : MonoBehaviour
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

    public void OpenNode(Node n, Node parent)
    {
        if (!n.GetOpen() && !n.GetClosed())
        {
            openNodes.Add(n);
            if (parent != null)
            {
                n.SetParent(parent);
            }
            n.SetOpen(true);
        }
    }

    public void CloseNode(Node n)
    {
        if (!n.GetClosed() && n.GetOpen())
        {
            closedNodes.Add(n);
            n.SetClosed(true);
            openNodes.Remove(n);
        }
    }

    public void OpenAdjacents(Node n)
    {
        for (int i = n.Adjacents().Count - 1; i >= 0; i--)
        {
            OpenNode(n.Adjacents()[i], n);
            Debug.Log("entering");
        }
    }

    public Node SelectNode()
    {
        return openNodes[openNodes.Count - 1];
    }

    public void CallForParents(Node n)
    {
        if (n.GetParent())
        {
            n.GetParent().SetAsPath(true);
            path.Add(n.GetParent());
            CallForParents(n.GetParent());
        }
    }

    public bool DestinyNode()
    {
        return selectedNode.GetDestiny();
    }
}
