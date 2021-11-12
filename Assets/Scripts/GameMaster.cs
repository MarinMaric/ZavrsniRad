using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    HidingController playerHidingScript;
    [SerializeField]
    CombatController playerCombatScript;
    [SerializeField]
    EnemyController robotController;

    GridGenerator gridGenerator;
    Recorder recorder;
    public float teleportDistance;

    Vector3 robotStart, playerStart;

    [SerializeField]
    List<GameObject> pickups;
    [SerializeField]
    List<Transform> dropPoints;

    void Awake()
    {
        robotStart = robotController.transform.position;
        playerStart = playerHidingScript.transform.position;

        gridGenerator = GetComponent<GridGenerator>();
        recorder = GetComponent<Recorder>();
    }

    private void Start()
    {
        GenerateDrops();
    }

    void Update()
    {
        CheckPlayerNoise();
        CheckTeleport();
        CheckHealths();
    }

    void GenerateDrops()
    {
        var listShuffled = pickups.OrderBy(x => Random.value).ToArray();

        for (int i = 0; i < dropPoints.Count; i++)
        {
            var item = Instantiate(listShuffled[i], dropPoints[i].position, dropPoints[i].rotation);
            item.GetComponent<ItemTrigger>().graphics = SetGraphics(item.name);
        }
    }

    Transform SetGraphics(string itemName)
    {
        List<string> names = new List<string>(){ "Flamethrower", "Shotgun", "Bomb", "Magnet", "Slowdown", "Heal" };
        var graphics = FindObjectOfType<CombatController>().transform.GetChild(0).GetChild(0);

        foreach (var n in names)
        {
            if (itemName.Contains(n))
            {
                var visuals = graphics.Find(n);
                return visuals;
            }
        }

        return null;
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

        if (roomDiff >= randomCriteria && playerHidingScript.currentRoom > robotController.activeRoom)
            Teleport();
        else if (roomDiff >= randomCriteria && playerHidingScript.currentRoom < robotController.activeRoom) 
            Backtrack();
        else if (robotController.backtracking && robotController.activeRoom == 1)
            robotController.backtracking = false;
    }

    void Teleport()
    {
        robotController.gameObject.SetActive(false);
        var robot = robotController.transform;

       
        foreach(var c in gridGenerator.changeTriggers)
        {
            if (c.index == playerHidingScript.currentRoom-1) //For the changer 1 room prior to the player
            {
                //Put robot behind the grid changer
                Vector3 behindDoor = c.transform.position - c.transform.forward * teleportDistance;
                robot.position = behindDoor;

                //Set up room and backtracking
                robotController.activeRoom = c.index;

                //Set target position to itself so that the Select Point node will tell the Pathfinder to find a new path
                robot.GetComponent<RobotMotor>().targetPosition = robot.position;
                //But for that to work, the spawnpoint needs to be set so that the Grid Generator can do it's thing
                GetComponent<GridGenerator>().ChangeSpawnPoint(c.index-1);

                break;
            }
        }

        robotController.gameObject.SetActive(true);
    }

    void Backtrack()
    {
        robotController.backtracking = true;
    }

    void CheckHealths()
    {
        if (playerCombatScript.health <= 0)
        {
            playerCombatScript.gameObject.SetActive(false);
            ResetLevel();
        }
        if (robotController.health <= 0)
        {
            robotController.gameObject.SetActive(false);

            //Reset the level but also store the recorded moves to be used later
            recorder.SaveProgress();
            ResetLevel();
        }
    }
    
    void ResetLevel()
    {
        //Return AI and player to original positions
        robotController.transform.position = robotStart;
        playerHidingScript.transform.position = playerStart;

        //Reset active rooms
        playerHidingScript.currentRoom = 1;
        robotController.activeRoom = 1;

        //Reset AI variables
        robotController.health = 100;
        robotController.killedPlayer = false;
        robotController.beganChasing = false;
        robotController.detectedPlayer = false;
        robotController.gameObject.GetComponent<RobotMotor>().targetPosition = robotStart;

        //Reset player variables
        playerCombatScript.ResetPlayer();
        playerHidingScript.ResetPlayer();

        //Reset all points
        var points = FindObjectsOfType<PointControl>();
        foreach (var p in points)
        {
            p.visited = false;
        }

        //Reset grid changers
        foreach (var changer in gridGenerator.changeTriggers) 
        {
            changer.robotPassed = false;
        }

        //Change to first spawn point
        gridGenerator.ChangeSpawnPoint(0);

        //Reactivate them both
        robotController.gameObject.SetActive(true);
        playerHidingScript.gameObject.SetActive(true);
    }
}
