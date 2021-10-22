using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridChanger : MonoBehaviour
{
    public int index;
    bool passed = false;

    public delegate void ChangeSpawn(int i);
    public event ChangeSpawn changeEvent;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!passed)
            {
                changeEvent(index);
                passed = true;
            }
            else
            {
                changeEvent(index - 1);
                passed = false;
            }
        }
    }
}
