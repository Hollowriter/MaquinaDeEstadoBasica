using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCreator : MonoBehaviour
{
    public int Cols = 10;
    public int Rows = 10;

    Node[,] nodes;

	// Use this for initialization
	void Start ()
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
}
