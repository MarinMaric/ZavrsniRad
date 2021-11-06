using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    Node root;
    bool startedBehavior;
    Coroutine behavior;

    public Node Root { get { return root; } }
    public EnemyController controller;

    void Start()
    {
        startedBehavior = false;
        CreateTree();
    }

    void Update()
    {
        if (!startedBehavior)
        {
            behavior = StartCoroutine(RunBehavior());
            startedBehavior = true;
        }
    }

    IEnumerator RunBehavior()
    {
        NodeState state = Root.Evaluate();
        while (state!=NodeState.SUCCESS)
        {
            yield return null;
            state = Root.Evaluate();
        }
    }

    void CreateTree()
    {
        List<Node> childNodes = new List<Node>()
        {
            new ActionNode(controller.SelectPoint),
            new ActionNode(controller.MoveToPoint),
            new ActionNode(controller.CheckForPlayer),
            new ActionNode(controller.ChasePlayer),
            new ActionNode(controller.AttackPlayer)
        }; 

        root = new SequenceNode(childNodes);
    }

    private void OnDisable()
    {
        startedBehavior = false;
    }
}
    