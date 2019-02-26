using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSelector : BNode
{
    protected List<BNode> m_nodes = new List<BNode>();

    public NSelector(List<BNode> b_nodes)
    {
        m_nodes = b_nodes;
    }

    public override int Evaluate()
    {
        foreach (BNode node in m_nodes)
        {
            switch (node.Evaluate())
            {
                case 0: // Failure
                    continue;
                case 1: // Success
                    m_nodeState = 1;
                    return m_nodeState;
                case 2: // Running
                    m_nodeState = 2;
                    return m_nodeState;
                default:
                    continue;
            }
        }
        m_nodeState = 0;
        return m_nodeState;
    }
}
