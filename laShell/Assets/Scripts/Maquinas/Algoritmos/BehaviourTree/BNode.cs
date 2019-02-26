using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BNode
{
    public delegate int NodeReturn();

    protected int m_nodeState; // 0: Failure, 1: Success, 2: Running

    public int GetNodeState() // Only a getter, we don't want random code to modify it
    {
        return m_nodeState;
    }

    public BNode()
    {

    }

    public abstract int Evaluate(); // Everything happens here
}
