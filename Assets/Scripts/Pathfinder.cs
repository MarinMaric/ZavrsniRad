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

    List<Field> path;
    List<Vector3> waypoints;

    Field startField, endField;

    public Transform robot;

    private void Start()
    {
        FindPath(robot.position, gridControl.targetWorld.position);
        waypoints = ReduceToWaypoints(path);

        gridControl.path = path;
    }

    private void Update()
    {
        if (gridControl.TargetField.position != endField.position)
        {
            FindPath(robot.position, gridControl.targetWorld.position);
            gridControl.path = path;
        }
    }

    public void FindPath(Vector3 start, Vector3 end)
    {
        open = new List<Field>();
        closed = new List<Field>();

        gridControl.GenerateGrid();
        startField = gridControl.PositionToField(start);
        endField = gridControl.PositionToField(end);

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
                path = RetracePath(startField, endField);
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

    List<Vector3> ReduceToWaypoints(List<Field> Path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 currentDir = Vector2.zero;

        for (int i = 1; i < Path.Count; i++)
        {
            Vector2 newDir = new Vector2(Path[i - 1].column - Path[i].column, Path[i - 1].row - Path[i].row);
            
            if (currentDir!=newDir)
            {
                waypoints.Add(Path[i].position);
            }

            currentDir = newDir;
        }

        return waypoints;
    }
}
