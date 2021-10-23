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
    [SerializeField]
    List<GridChanger> changeTriggers;
    public Transform targetWorld;
    public LayerMask obstacleMask;
    public List<Field> path;

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
        }

        spawnPoint = spawnPoints[0].transform;
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

        for (int i = 0; i < numberOfRows; i++)
        {
            //Set up row
            grid.gridRows.Add(new GridRow());
            grid.gridRows[i].fields = new List<Field>();

            for (int j = 0; j < fieldsPerRow; j++)
            {
                //crate individual field
                Vector3 position = gridCenter - new Vector3((fieldsPerRow / 2) * fieldWidth, 0, (numberOfRows / 2) * fieldWidth) + new Vector3(j * (fieldWidth + fieldPadding), 0, i * (fieldWidth + fieldPadding));
                bool traversible = Physics.CheckBox(position, Vector3.one * fieldWidth / 2, Quaternion.identity, obstacleMask, QueryTriggerInteraction.Ignore)?false:true;
                Field field = new Field(position, traversible, i, j); 
                grid.gridRows[i].fields.Add(field);    
            }
        }
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

        return grid.gridRows[(int)targetField.z].fields[(int)targetField.x];
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

    void ChangeSpawnPoint(int index)
    {
        spawnPoint = spawnPoints[index].transform;
        var pointScript = spawnPoint.GetComponent<SpawnPoint>();
        numberOfRows = pointScript.fieldsPerRow;
        fieldsPerRow = pointScript.fieldsPerRow;
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
