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
    [SerializeField]
    AudioSource robotAudio;

    GridGenerator gridGenerator;
    Recorder recorder;
    public float teleportDistance;

    Vector3 robotStart, playerStart;

    [SerializeField]
    List<GameObject> pickups;
    [SerializeField]
    List<Transform> dropPoints;

    [SerializeField]
    Animator fadeAnim;
    bool resetting;
    [SerializeField]
    GameObject wonDisplay;

    void Awake()
    {
        robotStart = robotController.transform.position;
        playerStart = playerHidingScript.transform.position;

        gridGenerator = GetComponent<GridGenerator>();
        recorder = GetComponent<Recorder>();
    }

    private void Start()
    {
        FindObjectOfType<EndTrigger>().victoryEvent += Victory;
        GenerateDrops();
    }

    void Update()
    {
        CheckRobotNoise();
        CheckPlayerNoise();
        CheckTeleport();
        CheckHealths();
    }

    IEnumerator TestTeleport()
    {
        yield return new WaitForSeconds(1);
        playerHidingScript.currentRoom = 6;
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

    void CheckRobotNoise()
    {
        if (Mathf.Abs(playerHidingScript.currentRoom - robotController.activeRoom) > 1 && !robotAudio.mute)
        {
            robotAudio.mute=true;
        }
        else if(Mathf.Abs(playerHidingScript.currentRoom - robotController.activeRoom) <= 1 && robotAudio.mute)
        {
            robotAudio.mute = false;
        }
    }

    void CheckTeleport()
    {
        int roomDiff = Mathf.Abs(playerHidingScript.currentRoom - robotController.activeRoom);
        int randomCriteria = Random.Range(2, 6);

        if (roomDiff >= randomCriteria && playerHidingScript.currentRoom > robotController.activeRoom && !robotController.backtracking)
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
            if (c.index == playerHidingScript.currentRoom-2) //For the changer 1 room prior to the player
            {
                //Put robot behind the grid changer
                Vector3 behindDoor = c.transform.position - c.transform.forward * teleportDistance;
                robot.position = behindDoor;

                //Set active room to be the one to which this door serves as an exit to prior
                robotController.activeRoom = c.index+1;

                //Set target position to itself so that the Select Point node will tell the Pathfinder to find a new path
                robot.GetComponent<RobotMotor>().targetPosition = robot.position;
                //But for that to work, the spawnpoint needs to be set so that the Grid Generator can do it's thing
                GetComponent<GridGenerator>().ChangeSpawnPoint(c.index);

                break;
            }
        }

        robotController.gameObject.SetActive(true);
    }

    void Backtrack()
    {
        robotController.backtracking = true;
        //TEST ONLY
        //playerHidingScript.currentRoom = robotController.activeRoom;
    }

    void CheckHealths()
    {
        if (playerCombatScript.health <= 0 && !resetting)
        {
            robotController.gameObject.SetActive(false);
            playerCombatScript.gameObject.SetActive(false);
            StartCoroutine(ResetLevel());

            Debug.Log("PLAYER DIED!");
        }
        if (robotController.health <= 0)
        {
            robotController.gameObject.SetActive(false);

            //Reset the level but also store the recorded moves to be used later
            recorder.SaveProgress();
            StartCoroutine(ResetLevel());

            Debug.Log("PLAYER WON!");
        }
    }
    
    IEnumerator ResetLevel()
    {
        //FadeEffect
        fadeAnim.SetBool("fadeIn", true);
        resetting = true;

        yield return new WaitForSeconds(2);

        fadeAnim.SetBool("fadeIn", false);

        //Return AI and player to original positions
        robotController.transform.position = robotStart;
        playerHidingScript.transform.position = playerStart;

        //Reset AI variables
        robotController.ResetRobot();
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

        resetting = false;
    }

    public void Victory()
    {
        robotController.gameObject.SetActive(false);
        playerCombatScript.gameObject.SetActive(false);

        StartCoroutine(WinSignal());
    }

    IEnumerator WinSignal()
    {
        wonDisplay.SetActive(true);

        yield return new WaitForSeconds(2);

        recorder.SaveProgress();
        StartCoroutine(ResetLevel());

        wonDisplay.SetActive(false);
    }
}
