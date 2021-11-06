using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridChanger : MonoBehaviour
{
    public int index;
    public bool robotPassed = false;
    private bool playerPassed = false;

    public delegate void ChangeSpawn(int i);
    public event ChangeSpawn changeEvent, clearEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetComponent<HidingController>().currentRoom += playerPassed ? -1 : 1;
            playerPassed = !playerPassed;
        }
        if (other.tag=="Robot")
        {
            var enemyController = FindObjectOfType<EnemyController>();

            if (!robotPassed)
            {
                changeEvent(index);
                robotPassed = true;
                enemyController.activeRoom++;
            }
            else
            {
                changeEvent(index - 1);
                robotPassed = false;
                if (!enemyController.backtracking)
                    enemyController.activeRoom--;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Robot")
        {
            var enemyController = FindObjectOfType<EnemyController>();

            //HACKING TEST
            if (!enemyController.backtracking)
            {
                if (enemyController.activeRoom != index + 1)
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
                }
            }
            else
            {
                enemyController.backtracking = false;

                clearEvent.Invoke(index);
            }
            //else
            //{
            //    if (robotPassed)
            //    {
            //        changeEvent(index - 1);
            //        robotPassed = false;
            //    }
            //}
        }
    }
}
