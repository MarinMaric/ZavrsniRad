using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField]
    GridGenerator gridControl;

    List<Field> open;
    List<Field> closed;

    List<Field> Path;

    private void Start()
    {
        gridControl.GenerateGrid();
    }

    private void Update()
    {
        FindPath(transform.position, gridControl.dummyWorld.position);
    }

    public void FindPath(Vector3 start, Vector3 end)
    {
        open = new List<Field>();
        closed = new List<Field>();

        gridControl.GenerateGrid();
        Field startField = gridControl.PositionToField(start);
        Field endField = gridControl.PositionToField(end);

        //add the starting field to open
        open.Add(startField);

        while (open.Count>0)
        {
            Field current = open[0];
            for (int i = 0; i < open.Count; i++)
            {
                if(open[i].fCost<=current.fCost && open[i].hCost < current.hCost)
                {
                    current = open[i];
                }
            }
            open.Remove(current);
            closed.Add(current);

            if (current == endField)
            {
                gridControl.path = RetracePath(startField, endField);
                return;
            }

            List<Field> neighbours = gridControl.GetNeighbors(current);
            foreach (var n in neighbours)
            {
                if (!n.traversible || closed.Contains(n))
                {
                    continue;
                }

                int newCostToNeighbour = current.gCost + FieldDistance(current, n);
                if (newCostToNeighbour < n.gCost || !open.Contains(n))
                {
                    n.gCost = newCostToNeighbour;
                    n.hCost = FieldDistance(n, endField);
                    n.parent = current;

                    if (!open.Contains(n))
                        open.Add(n);
                }
            }
        }
    }

    int FieldDistance(Field origin, Field target)
    {
        int rowDiff = Mathf.Abs(origin.row - target.row);
        int columnDiff = Mathf.Abs(origin.column - target.column);
        int distance;

        if (rowDiff > columnDiff)
        {
            distance = 14 * columnDiff + 10 * (rowDiff - columnDiff);
        }
        else
        {
            distance = 14 * rowDiff + 10 * (columnDiff - rowDiff);
        }

        return distance;
    }

    List<Field> RetracePath(Field startField, Field endField)
    {
        List<Field> path = new List<Field>();
        Field current = endField;

        while (current != startField)
        {
            path.Add(current);
            current = current.parent;
        }
        path.Reverse();

        return path;
    }
}
