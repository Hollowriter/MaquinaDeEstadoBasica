using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSecuence : BNode
{
    protected List<BNode> m_nodes = new List<BNode>();

    public NSecuence(List<BNode> b_nodes)
    {
        m_nodes = b_nodes;
    }

    public override int Evaluate()
    {
        bool anyChildRunning = false;
        foreach (BNode node in m_nodes)
        {
            switch (node.Evaluate())
            {
                case 0: // Failure
                    m_nodeState = 0;
                    return m_nodeState;
                case 1: // Success
                    continue;
                case 2: // Running
                    anyChildRunning = true;
                    continue;
                default:
                    m_nodeState = 1;
                    return m_nodeState;
            }
        }
        m_nodeState = anyChildRunning ? 2 : 1;
        return m_nodeState;
    }
}
