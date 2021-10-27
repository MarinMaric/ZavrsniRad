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
            if (FindObjectOfType<EnemyController>().detectedPlayer != true)
                return;
        }
        if (other.tag=="Robot")
        {
            if (!passed)
            {
                changeEvent(index);
                passed = true;
                FindObjectOfType<EnemyController>().activeRoom++;
            }
            else
            {
                changeEvent(index - 1);
                passed = false;
                FindObjectOfType<EnemyController>().activeRoom--;
            }

            //Debug.Log("Moved to: " + FindObjectOfType<EnemyController>().activeRoom);
        }
    }
}
