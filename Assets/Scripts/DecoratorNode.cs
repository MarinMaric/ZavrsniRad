using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RepeaterNode : Node
{
    int n;
    Node childNode;

    public RepeaterNode(Node node, int _n)
    {
        childNode = node;
        n = _n;
    }

    public override NodeState Evaluate()
    {
        while(childNode.State==NodeState.FAILURE)
            childNode.Evaluate();
                
        return childNode.State;
    }
}
