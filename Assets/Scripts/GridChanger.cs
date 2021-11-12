using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridChanger : MonoBehaviour
{
    public int index;
    public bool robotPassed = false;

    public delegate void ChangeSpawn(int i);
    public event ChangeSpawn changeEvent, clearEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var hidingScript = other.transform.GetComponent<HidingController>();

            if (GoingIn(hidingScript.movementVector, other.transform))
            {
                hidingScript.currentRoom = index;
            }
            else 
            {
                hidingScript.currentRoom = index + 1;
            } 
        }
        if (other.tag=="Robot")
        {
            var enemyController = FindObjectOfType<EnemyController>();

            if (GoingIn(other.transform))
            {
                changeEvent(index-1);
                enemyController.activeRoom = index;
            }
            else
            {
                changeEvent(index);
                enemyController.activeRoom = index + 1;
            }

            #region old system
            //if (!robotPassed)
            //{
            //    changeEvent(index);
            //    robotPassed = true;
            //    enemyController.activeRoom++;
            //}
            //else
            //{
            //    changeEvent(index - 1);
            //    robotPassed = false;
            //    if (!enemyController.backtracking)
            //        enemyController.activeRoom--;
            //}
            #endregion
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Robot")
        {
            var enemyController = FindObjectOfType<EnemyController>();

            if (GoingIn(other.transform)) {
                if (enemyController.activeRoom != index)
                {
                    changeEvent(index - 1);
                    enemyController.activeRoom = index;
                }
            }
            else
            {
                if (enemyController.activeRoom != index+1)
                {
                    changeEvent(index);
                    enemyController.activeRoom = index + 1;
                }
            }

            #region old system
            //HACKING TEST
            //if (!enemyController.backtracking)
            //{
            //    if (enemyController.activeRoom != index + 1)
            //    {
            //        if (!robotPassed)
            //        {
            //            changeEvent(index);
            //            robotPassed = true;
            //            FindObjectOfType<EnemyController>().activeRoom++;
            //        }
            //        else
            //        {
            //            changeEvent(index - 1);
            //            robotPassed = false;
            //            FindObjectOfType<EnemyController>().activeRoom--;
            //        }
            //    }
            //}
            //else
            //{
            //    enemyController.backtracking = false;

            //    clearEvent.Invoke(index);
            //}
            ////else
            ////{
            ////    if (robotPassed)
            ////    {
            ////        changeEvent(index - 1);
            ////        robotPassed = false;
            ////    }
            ////}
            #endregion
        }
    }

    bool GoingIn(Vector2 movement, Transform player)
    {
        if ((movement.y > 0 && Vector3.Angle(player.forward, transform.forward) <= 60f)
            || (movement.y < 0 && Vector3.Angle(-player.forward, transform.forward) <= 60f))
        {
            return true;
        }
        else if ((movement.x > 0 && Vector3.Angle(player.right, transform.forward) <= 60f)
            || (movement.x < 0 && Vector3.Angle(-player.right, transform.forward) <= 60f))
        {
            return true;
        }

        return false;
    }

    bool GoingIn(Transform robot)
    {
        if (Vector3.Angle(robot.forward, transform.forward) <= 90)
            return true;
        else
            return false;
    }
}
