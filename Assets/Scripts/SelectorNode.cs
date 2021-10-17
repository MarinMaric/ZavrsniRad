using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SelectorNode : Node
{
    public List<Node> ChildNodes { get; set; }

    public SelectorNode(List<Node> childNodes)
    {
        ChildNodes = childNodes;
    }

    public override NodeState Evaluate()
    {
        foreach (var node in ChildNodes)
        {
            switch (node.State)
            {
                case NodeState.FAILURE:
                    continue;
                case NodeState.SUCCESS:
                    nodeState = NodeState.SUCCESS;
                    return State;
                case NodeState.RUNNING:
                    nodeState = NodeState.RUNNING;
                    return State;
                default:
                    continue;
            }
        }
        nodeState = NodeState.FAILURE;
        return State;
    }
}

