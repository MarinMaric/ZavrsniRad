using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Field
{
    public Vector3 position;
    public int hCost, gCost;
    public int row, column;
    public bool traversible;
    public Field parent;

    public Field(Vector3 position, bool traversible, int row, int column, int hCost =0, int gCost=0)
    {
        this.position = position;
        this.hCost = hCost;
        this.gCost = gCost;
        this.row = row;
        this.column = column;
        this.traversible = traversible;
    }

    public int fCost { get {
            return hCost + gCost;
        } 
    }
}
