using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : BNode
{
    public delegate int ActionNodeDelegate();

    private ActionNodeDelegate m_action;

    public ActionNode(ActionNodeDelegate action)
    {
        m_action = action;
    }

    public override int Evaluate()
    {
        switch (m_action())
        {
            case 0:
                m_nodeState = 0;
                return m_nodeState;
            case 1:
                m_nodeState = 1;
                return m_nodeState;
            case 2:
                m_nodeState = 2;
                return m_nodeState;
            default:
                m_nodeState = 1;
                return m_nodeState;
        }
    }
}
