using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item")]
public class Item:ScriptableObject
{
    public int damage;
    public int currentAmmo, totalAmmo;
    public ItemType itemType;
    public bool equipped;
    public Transform graphics;
}
