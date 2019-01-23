using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCreator : MonoBehaviour
{
    public int Cols = 10;
    public int Rows = 10;

    Node[,] nodes;
    Node nodeOrigin = null;
    /*[SerializeField]
    GameObject thing;*/

	// Use this for initialization
	void Awake ()
    {
        nodes = new Node[Cols, Rows];

        for(int row = 0; row < Rows; row++)
        {
            for(int col = 0; col < Cols; col++)
            {
                GameObject go = new GameObject("Nodo");
                nodes[col, row] = go.AddComponent<Node>();
                go.transform.position = new Vector3(col * 2.0f, 0.0f, row * 2.0f);
            }
        }

        for(int row = 0; row < Rows; row++)
        {
            for(int col = 0; col < Cols; col++)
            {
                if(col > 0)
                {
                    nodes[col - 1, row].AddAdjacent(nodes[col, row]);
                    nodes[col, row].AddAdjacent(nodes[col-1, row]);
                }

                if(row > 0)
                {
                    nodes[col, row - 1].AddAdjacent(nodes[col, row]);
                    nodes[col, row].AddAdjacent(nodes[col, row - 1]);
                }

                if (col < Cols - 1)
                {
                    nodes[col + 1, row].AddAdjacent(nodes[col, row]);
                    nodes[col, row].AddAdjacent(nodes[col + 1, row]);
                }

                if(row < Rows- 1)
                {
                    nodes[col, row + 1].AddAdjacent(nodes[col, row]);
                    nodes[col, row].AddAdjacent(nodes[col, row + 1]);
                }
            }
        }
    }

    public void ResetAllNodes()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Cols; col++)
            {
                nodes[col, row].SetOpen(false);
                nodes[col, row].SetClosed(false);
                nodes[col, row].SetDestiny(false);
                nodes[col, row].SetAsPath(false);
                nodes[col, row].SetParent(null);
                nodes[col, row].SetChild(null);
            }
        }
    }

    public Node GetNodeByPosition(Vector3 pos)
    {
        nodeOrigin = nodes[0, 0];
        float dist = (pos - nodes[0, 0].transform.position).magnitude;

        for(int c = 0; c < Cols; c++)
        {
            for(int r = 0; r < Rows; r++)
            {
                Node n = nodes[c, r];
                float newDist = (pos - n.transform.position).magnitude;

                if(dist > newDist)
                {
                    nodeOrigin = n;
                    dist = newDist;
                }
            }
        }
        return nodeOrigin;
    }
}
