using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridChanger : MonoBehaviour
{
    public int index;
    bool robotPassed = false, playerPassed = false;

    public delegate void ChangeSpawn(int i);
    public event ChangeSpawn changeEvent;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetComponent<HidingController>().currentRoom += playerPassed ? -1 : 1;
            playerPassed = !playerPassed;
        }
        if (other.tag=="Robot")
        {
            if (!robotPassed)
            {
                changeEvent(index);
                robotPassed = true;
                FindObjectOfType<EnemyController>().activeRoom++;
            }
            else
            {
                changeEvent(index - 1);
                robotPassed = false;
                FindObjectOfType<EnemyController>().activeRoom--;
            }

            //Debug.Log("Moved to: " + FindObjectOfType<EnemyController>().activeRoom);
        }
    }
}
