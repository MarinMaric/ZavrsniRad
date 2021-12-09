using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    Grid grid;
    public int numberOfRows;
    public float fieldWidth, fieldPadding;
    public int fieldsPerRow;
    Vector3 bottomLeft, gridCenter;
    Vector3 gridSize;
    Transform spawnPoint;
    [SerializeField]
    List<Transform> spawnPoints;
    public List<GridChanger> changeTriggers;
    public Transform targetWorld;
    public LayerMask obstacleMask;
    public List<Field> path;
    public bool generatedGrid;

    public Field TargetField { 
        get {
            return PositionToField(targetWorld.position);
        } 
    }

    private void Start()
    {
        foreach(var t in changeTriggers)
        {
            t.changeEvent += ChangeSpawnPoint;
            t.clearEvent += ClearSubsequent;
        }

        spawnPoint = spawnPoints[0].transform;
        numberOfRows = spawnPoint.GetComponent<SpawnPoint>().numberOfRows;
        fieldsPerRow = spawnPoint.GetComponent<SpawnPoint>().fieldsPerRow;
    }

    public void GenerateGrid()
    {
        float gridSizeZ = numberOfRows * fieldWidth;
        float gridSizeX = fieldsPerRow * fieldWidth;
        gridSize = new Vector3(gridSizeX, 0, gridSizeZ);
        gridCenter = spawnPoint.position;
        bottomLeft = gridCenter - new Vector3(gridSizeX, 0, gridSizeZ);
      
        grid = new Grid();
        grid.gridRows = new List<GridRow>();
        Vector3 displacementLeft = new Vector3((fieldsPerRow / 2) * fieldWidth, 0, (numberOfRows / 2) * fieldWidth);
        Vector3 rowStart = gridCenter - displacementLeft;

        for (int i = 0; i < numberOfRows; i++)
        {
            //Set up row
            grid.gridRows.Add(new GridRow());
            grid.gridRows[i].fields = new List<Field>();


            for (int j = 0; j < fieldsPerRow; j++)
            {
                //crate individual field
                Vector3 position = rowStart + new Vector3(j * fieldWidth, 0, i * fieldWidth);
                bool traversible = CheckForObstacle(position);
                Field field = new Field(position, traversible, i, j); 
                grid.gridRows[i].fields.Add(field);    
            }
        }

        generatedGrid = true;
    }

    bool CheckForObstacle(Vector3 position)
    {
        return Physics.CheckBox(position, 
            Vector3.one * fieldWidth / 2, Quaternion.identity, obstacleMask, 
            QueryTriggerInteraction.Ignore) 
            ? false : true;
    }

    public Field PositionToField(Vector3 worldPosition)
    {
        //limit change of field for even coordinates 
        if(worldPosition.x%2!=0 && worldPosition.y % 2 != 0)
        {
            worldPosition += Vector3.one;
        }

        //first calculate the center field index
        Vector3 centerIndex = new Vector3(Mathf.Round(fieldsPerRow/ 2), 0, Mathf.Round(numberOfRows / 2));
        Vector3 distanceWorld = worldPosition - gridCenter;
        Vector3 distanceGrid = distanceWorld / fieldWidth;
        Vector3 targetField = centerIndex + distanceGrid;

        if (targetWorld.tag == "Player")
        {
            if ((targetField.z >= 0 && targetField.z < numberOfRows) && 
                    (targetField.x >= 0 && targetField.x < fieldsPerRow))
                return grid.gridRows[(int)targetField.z].fields[(int)targetField.x];
            else return null;
        }

        return grid.gridRows[(int)Mathf.Clamp(targetField.z, 0, numberOfRows - 1)]
            .fields[(int)Mathf.Clamp(targetField.x, 0, fieldsPerRow - 1)];
    }

    public bool FieldsClose(Vector3 positionA, Vector3 positionB)
    {
        Field fieldA = PositionToField(positionA);
        Field fieldB = PositionToField(positionB);

        if (Mathf.Abs(fieldA.row - fieldB.row) <= 1 && Mathf.Abs(fieldA.column - fieldB.column) <= 1)
            return true;
        else
            return false;
    }

    public List<Field> GetNeighbors(Field field)
    {
        List<Field> neighbors = new List<Field>();

        for (int r = -1; r <= 1; r++)
        {
            for (int c = -1; c <= 1; c++)
            {
                if (r == 0 && c == 0)
                    continue;

                int row = field.row + r;
                int column = field.column + c;

                if (row >= 0 && row < numberOfRows && column >= 0 && column < fieldsPerRow)
                {
                    neighbors.Add(grid.gridRows[row].fields[column]);
                }
            }
        }

        return neighbors;
    }

    public void ChangeSpawnPoint(int index)
    {
        spawnPoint = spawnPoints[index].transform;
        var pointScript = spawnPoint.GetComponent<SpawnPoint>();
        numberOfRows = pointScript.numberOfRows;
        fieldsPerRow = pointScript.fieldsPerRow;
        targetWorld = null;

        var exitPoints = GameObject.FindGameObjectsWithTag("Exit");
        foreach (var p in exitPoints)
        {
            p.GetComponent<PointControl>().visited = false;
        }
    }

    public void ClearSubsequent(int index)
    {
        for (int i = index; i < changeTriggers.Count; i++)
        {
            changeTriggers[i].robotPassed = false;
        }
    }

    public bool CheckGrid()
    {
        return grid != null;
    }

    private void OnDrawGizmos()
    {
        if (grid != null)
        {
            Field targetField = PositionToField(targetWorld.position);
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < fieldsPerRow; j++)
                {
                    if (grid.gridRows[i].fields[j] == targetField || !grid.gridRows[i].fields[j].traversible)
                        Gizmos.color = Color.red;
                    else
                        Gizmos.color = Color.white;

                    if (path != null)
                        if (path.Contains(grid.gridRows[i].fields[j]))
                            Gizmos.color = Color.black;

                    Gizmos.DrawCube(grid.gridRows[i].fields[j].position, Vector3.one * fieldWidth);
                }
            }

            //if (path != null)
            //{
            //    Gizmos.color = Color.black;

            //    foreach (var p in path)
            //    {
            //        Gizmos.DrawCube(p.position, Vector3.one * fieldWidth);
            //    }
            //}
        }
    }

    #region nested classes
    class Grid
    {
        public List<GridRow> gridRows;
    }
    class GridRow
    {
        public List<Field> fields;
    }
    #endregion
}
