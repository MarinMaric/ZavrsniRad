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
    public Transform dummyWorld;

    private void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        float gridSizeZ = numberOfRows * fieldWidth;
        float gridSizeX = fieldsPerRow * fieldWidth;
        gridSize = new Vector3(gridSizeX, 0, gridSizeZ);
        gridCenter = transform.position;
        bottomLeft = gridCenter - new Vector3(gridSizeX, 0, gridSizeZ);
      
        grid = new Grid();
        grid.gridRows = new List<GridRow>();

        for (int i = 0; i < numberOfRows; i++)
        {
            //Set up row
            grid.gridRows.Add(new GridRow());
            grid.gridRows[i].fields = new List<Vector3>();

            for (int j = 0; j < fieldsPerRow; j++)
            {
                //crate individual field
                Vector3 field = gridCenter - new Vector3((fieldsPerRow/2)*fieldWidth, 0, (numberOfRows/2)*fieldWidth) + new Vector3(j*(fieldWidth+fieldPadding), 0, i*(fieldWidth+fieldPadding));
                grid.gridRows[i].fields.Add(field);    
            }
        }
    }

    public Vector3 FromWorldPointToField(Vector3 worldPosition)
    {
        float gridWorldSizeX = fieldsPerRow * fieldWidth;
        float gridWorldSizeZ = numberOfRows * fieldWidth;
        float percentX = (worldPosition.x + gridWorldSizeX / 2) / gridWorldSizeX;
        float percentY = (worldPosition.z + gridWorldSizeZ / 2) / gridWorldSizeZ;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((fieldsPerRow - 1) * percentX);
        int z = Mathf.RoundToInt((numberOfRows - 1) * percentY);
        return grid.gridRows[z].fields[x];
    }
    public Vector3 FromWorldPointCustom(Vector3 worldPosition)
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

    private void OnDrawGizmos()
    {
        if (grid != null)
        {
            Vector3 dummyPos = FromWorldPointCustom(dummyWorld.position);
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < fieldsPerRow; j++)
                {
                    if (grid.gridRows[i].fields[j] == dummyPos)
                        Gizmos.color = Color.red;
                    else
                        Gizmos.color = Color.white;

                    Gizmos.DrawCube(grid.gridRows[i].fields[j], Vector3.one * fieldWidth);
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
        public List<Vector3> fields;
    }
    #endregion
}
