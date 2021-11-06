using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    HidingController playerHidingScript;
    [SerializeField]
    EnemyController robotController;
    GridGenerator gridGenerator;
    public float teleportDistance;

    void Awake()
    {
        gridGenerator = GetComponent<GridGenerator>();
    }

    void Update()
    {
        CheckPlayerNoise();
        CheckTeleport();
    }

    void CheckPlayerNoise()
    {
        if (playerHidingScript.currentRoom == robotController.activeRoom && playerHidingScript.moving && !playerHidingScript.sneaking)
        {
            robotController.detectedPlayer = true;
            robotController.ChasePlayer();
        }
    }

    void CheckTeleport()
    {
        int roomDiff = Mathf.Abs(playerHidingScript.currentRoom - robotController.activeRoom);
        int randomCriteria = Random.Range(2, 6);

        if (roomDiff >= randomCriteria)
           Teleport();
    }

    void Teleport()
    {
        robotController.gameObject.SetActive(false);
        var robot = robotController.transform;

       
        foreach(var c in gridGenerator.changeTriggers)
        {
            if (c.index == playerHidingScript.currentRoom)
            {
                //Put player behind the grid changer
                Vector3 behindDoor = c.transform.position - c.transform.forward * teleportDistance;
                robot.position = behindDoor;

                //Set up room and backtracking
                robotController.activeRoom = c.index;

                robotController.backtracking = true;
            }
        }

        //Set target position to itself so that the Select Point node will tell the Pathfinder to find a new path
        robot.GetComponent<RobotMotor>().targetPosition = robot.position;
        //But for that to work, the spawnpoint needs to be set so that the Grid Generator can do it's thing
        GetComponent<GridGenerator>().ChangeSpawnPoint(0);

        robotController.gameObject.SetActive(true);
    }
}
