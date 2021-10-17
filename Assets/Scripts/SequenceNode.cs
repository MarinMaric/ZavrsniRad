using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SequenceNode:Node
{
    public List<Node> ChildNodes { get; set; }
    bool anyChildRunning = false;

    public SequenceNode(List<Node> childNodes)
    {
        ChildNodes = childNodes;
    }

    public override NodeState Evaluate()
    {
        foreach (var node in ChildNodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    nodeState=NodeState.FAILURE;
                    return nodeState;
                case NodeState.SUCCESS:
                    continue;
                case NodeState.RUNNING:
                    anyChildRunning = true;
                    continue;
                default:
                    nodeState = NodeState.SUCCESS;
                    return State;
            }
        }

        anyChildRunning = ChildNodes.Any(x => x.State == NodeState.RUNNING);

        nodeState = anyChildRunning?NodeState.RUNNING:NodeState.SUCCESS;
        return State;
    }
}

