using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NDecorator : BNode
{
    private BNode m_node;

    public BNode node
    {
        get { return m_node; }
    }

    public NDecorator(BNode b_node)
    {
        m_node = b_node;
    }

    public override int Evaluate()
    {
        switch (m_node.Evaluate())
        {
            case 0: // Failure
                m_nodeState = 1;
                return m_nodeState;
            case 1: // Success
                m_nodeState = 0;
                return m_nodeState;
            case 2: // Running
                m_nodeState = 2;
                return m_nodeState;
        }
        m_nodeState = 1;
        return m_nodeState;
    }
}
