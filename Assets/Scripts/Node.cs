using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
    public delegate NodeState NodeReturn();

    protected NodeState nodeState;
    public NodeState State { 
        get {
            return nodeState;
        }
    }

    public abstract NodeState Evaluate();
}


public enum NodeState
{
    FAILURE, SUCCESS, RUNNING
}