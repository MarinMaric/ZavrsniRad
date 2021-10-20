using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    Grid grid;
    public int numberOfRows;
    public float fieldWidth, fieldPadding;
    public int fieldsPerRow;
    public Vector3 bottomLeft, gridCenter;

    private void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        float gridSizeZ = numberOfRows * fieldWidth;
        float gridSizeX = fieldsPerRow * fieldWidth;
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

    private void OnDrawGizmos()
    {
        if (grid != null)
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < fieldsPerRow; j++)
                {
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
