using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ActionNode : Node
{
    public delegate NodeState ActionNodeDelegate();
    ActionNodeDelegate nodeAction;

    public ActionNode(ActionNodeDelegate action)
    {
        nodeAction = action;
    }

    public override NodeState Evaluate()
    {
        switch (nodeAction())
        {
            case NodeState.FAILURE:
                nodeState=NodeState.FAILURE;
                return State;
            case NodeState.SUCCESS:
                nodeState=NodeState.SUCCESS;
                return State;
            case NodeState.RUNNING:
                nodeState = NodeState.RUNNING;
                return State;
            default:
                nodeState = NodeState.FAILURE;
                return State;
        }
    }
}
