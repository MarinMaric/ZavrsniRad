using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    RobotMotor motor;
    [HideInInspector]
    public Transform targetPosition;
    [HideInInspector]
    public bool detectedPlayer, beganChasing;
    public float selectableRadius, detectableRadius, lookAngle;
    public LayerMask pointsLayer, playerLayer, obstacleLayer;
    public Recorder recorder;
    List<StoredItem> priorities;
    Dictionary<string, int> immunities;
    List<PointControl> points = new List<PointControl>();
    int priorityIndex = -1;
    public int activeRoom = 1;
    public bool backtracking = false;

    public delegate void ResetPath();
    public event ResetPath resetPathEvent;
    public delegate void LostPlayer();
    public event LostPlayer lostPlayerEvent;
    List<int> checkedRooms;

    public int health = 100, attackRange;
    public int damage = 40, delay = 2;
    public bool inRange, attacked = false;
    [HideInInspector]
    public bool killedPlayer = false;

    GridGenerator gc;

    void Start()
    {
        targetPosition = transform;
        motor.targetPosition = transform.position;
        priorities = recorder.GetPriorities();
        immunities = recorder.GetImmunities();

        FindObjectOfType<CombatController>().dealtDamageEvent += TakeDamage;

        checkedRooms = new List<int>();
        gc = FindObjectOfType<GridGenerator>();
    }
    
    public NodeState SelectPoint()
    {
        if (transform.position == motor.targetPosition && !detectedPlayer)
        {
            #region select from priorities
            if (points.Count == 0 && priorityIndex+1 < priorities.Count)
            {
                priorityIndex++;
            }else if (priorityIndex == -1)
            {
                priorityIndex++;
            }

            //If point is reached and no player has been found, go to random unvisited point with said tag
            var hits = Physics.SphereCastAll(transform.position, selectableRadius, transform.forward, selectableRadius, pointsLayer.value, QueryTriggerInteraction.Collide);
            points.Clear();
            foreach (var h in hits)
            {
                if (h.collider.tag == priorities[priorityIndex].Name && h.collider.GetComponent<PointControl>().visited == false && h.collider.GetComponent<PointControl>().roomId == activeRoom)
                {
                    if(priorities[priorityIndex].Name == "Normal")
                    {
                        points.Add(h.collider.GetComponent<PointControl>());
                    }
                    else if(priorities[priorityIndex].Name!="Normal" && Random.Range(1, 6) <= priorities[priorityIndex].Value)
                    {
                        points.Add(h.collider.GetComponent<PointControl>());
                    }
                    else
                    {
                        h.collider.GetComponent<PointControl>().visited = true;
                    }
                }
            }

            if (points.Count > 0)
            {
                int index= (int)Random.Range(0, points.Count); 
                while (points[index].transform == targetPosition)
                {
                    index = (int)Random.Range(0, points.Count);
                }
                targetPosition = points[index].transform;
                points.RemoveAt(index);
                resetPathEvent.Invoke();
            }
            #endregion
            #region leave room
            else if (priorityIndex == priorities.Count - 1)
            {
                #region comment
                //TEST ONLY
                //if(activeRoom==9)
                //    SceneManager.LoadScene(0);

                //if all the points in the room have been checked then mark the whole room as checked
                //checkedRooms.Add(activeRoom);

                //prepare priority for next room 
                #endregion
                priorityIndex = -1;

                //move on to next room (later I can make it go back as well)
                foreach (var h in hits)
                {
                    //h.collider.GetComponent<PointControl>().visited = false;
                    if (!backtracking)
                    {
                        if (h.collider.tag == "Exit" && h.collider.GetComponent<PointControl>().roomId == activeRoom && h.collider.GetComponent<PointControl>().visited == false)
                            points.Add(h.collider.GetComponent<PointControl>());
                    }
                    else
                    {
                        if (h.collider.tag == "Back" && h.collider.GetComponent<PointControl>().roomId == activeRoom)
                            points.Add(h.collider.GetComponent<PointControl>());
                    }
                }

                if (points.Count > 0)
                {
                    targetPosition = points[0].transform;
                    resetPathEvent.Invoke();
                }
                else
                {
                    //since there are no more rooms switch to backtracking mode
                    //foreach (var h in hits)
                    //{
                    //    if (h.collider.tag == "Exit" && h.collider.GetComponent<PointControl>().roomId == activeRoom-1)
                    //    {
                    //        points.Add(h.collider.GetComponent<PointControl>());
                    //    }
                    //}
                }
            }
            #endregion
        }

        return NodeState.SUCCESS;
    }

    public NodeState IsPositionReached()
    {
        if (transform.position != motor.targetPosition || detectedPlayer)
            return NodeState.SUCCESS;
        else return NodeState.FAILURE;
    }
    public NodeState SelectFromPriorities()
    {
        if (points.Count == 0 && priorityIndex + 1 < priorities.Count)
        {
            priorityIndex++;
        }
        else if (priorityIndex == -1)
        {
            priorityIndex++;
        }

        //If point is reached and no player has been found, go to random unvisited point with said tag
        var hits = Physics.SphereCastAll(transform.position, selectableRadius, transform.forward, selectableRadius, pointsLayer.value, QueryTriggerInteraction.Collide);
        points.Clear();
        foreach (var h in hits)
        {
            if (h.collider.tag == priorities[priorityIndex].Name && h.collider.GetComponent<PointControl>().visited == false && h.collider.GetComponent<PointControl>().roomId == activeRoom)
            {
                if (priorities[priorityIndex].Name == "Normal")
                {
                    points.Add(h.collider.GetComponent<PointControl>());
                }
                else if (priorities[priorityIndex].Name != "Normal" && Random.Range(1, 6) <= priorities[priorityIndex].Value)
                {
                    points.Add(h.collider.GetComponent<PointControl>());
                }
                else
                {
                    h.collider.GetComponent<PointControl>().visited = true;
                }
            }
        }

        if (points.Count > 0)
        {
            int index = (int)Random.Range(0, points.Count);
            while (points[index].transform == targetPosition)
            {
                index = (int)Random.Range(0, points.Count);
            }
            targetPosition = points[index].transform;
            points.RemoveAt(index);
            resetPathEvent.Invoke();
        }
        else if (priorityIndex == priorities.Count - 1)
        {
            return NodeState.FAILURE;
        }

        return NodeState.SUCCESS;
    }
    public NodeState LeaveRoom()
    {
        priorityIndex = -1;

        var hits = Physics.SphereCastAll(transform.position, selectableRadius, transform.forward, selectableRadius, pointsLayer.value, QueryTriggerInteraction.Collide);

        //move on to next room (later I can make it go back as well)
        foreach (var h in hits)
        {
            //h.collider.GetComponent<PointControl>().visited = false;
            if (!backtracking)
            {
                if (h.collider.tag == "Exit" && h.collider.GetComponent<PointControl>().roomId == activeRoom && h.collider.GetComponent<PointControl>().visited == false)
                    points.Add(h.collider.GetComponent<PointControl>());
            }
            else
            {
                if (h.collider.tag == "Back" && h.collider.GetComponent<PointControl>().roomId == activeRoom)
                    points.Add(h.collider.GetComponent<PointControl>());
            }
        }

        if (points.Count > 0)
        {
            targetPosition = points[0].transform;
            resetPathEvent.Invoke();
        }

        return NodeState.SUCCESS;
    }

    public NodeState MoveToPoint()
    {
        if (beganChasing)
            return NodeState.SUCCESS;

        if (transform.position != motor.targetPosition && !detectedPlayer)
        {
            motor.Move();
            return NodeState.RUNNING;
        }
        else if(transform.position == motor.targetPosition)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }

     public NodeState CheckForPlayer()
     {
        if (beganChasing)
            return NodeState.SUCCESS;

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, detectableRadius, transform.forward, 0, playerLayer.value, QueryTriggerInteraction.Collide);
        var gc = FindObjectOfType<GridGenerator>();

        foreach (RaycastHit hit in hits)
        {
            if (!gc.generatedGrid)
            {
                return NodeState.FAILURE;
            }

            Vector3 direction = (hit.transform.position - transform.position).normalized;
            float targetAngle = Vector3.Angle(transform.forward, direction);
            Debug.DrawRay(transform.position, direction, Color.red);
            if (targetAngle < lookAngle / 2)
            {
                //Just in case the robot picks it up when it doesn't even have that priority
                var hc = FindObjectOfType<HidingController>();
                
                if(hc.hiding && hc.currentRoom==activeRoom)
                {
                    if (!priorities.Contains(hc.hidingSpot) ||  !gc.FieldsClose(hc.spotTransform.position, targetPosition.position))
                    {
                        var spot = gc.PositionToField(hc.spotTransform.position);
                        var target = gc.PositionToField(targetPosition.position);
                        Debug.Log("(" + spot.row + ", " + spot.column + " vs " + "(" + target.row + ", " + target.column + ")");

                        detectedPlayer = false;
                        return NodeState.FAILURE;
                    }
                }
                else if(!hc.hiding && hc.currentRoom!=activeRoom)
                {
                    detectedPlayer = false;
                    return NodeState.FAILURE;
                }

                RaycastHit checkObstacle;
                Ray ray = new Ray(transform.position, direction);
                if (Physics.Raycast(ray, out checkObstacle, detectableRadius, ~pointsLayer, QueryTriggerInteraction.Ignore))
                {
                    //Debug.Log(checkObstacle.collider.tag);
                    if (checkObstacle.collider.tag == "Player")
                    {
                        detectedPlayer = true;
                    }
                }
            }
        }
        
        if (detectedPlayer)
            return NodeState.SUCCESS;
        else
            return NodeState.FAILURE;
    }

    public NodeState ChasePlayer()
    {
        var hc = FindObjectOfType<HidingController>();
        if (hc.hiding && !priorities.Contains(FindObjectOfType<HidingController>().hidingSpot))
        {
            beganChasing = false;
            detectedPlayer = false;

            if (hc.currentRoom == activeRoom)
            {
                targetPosition = transform;
            }
            else
            {
                GameObject[] exitPoints = GameObject.FindGameObjectsWithTag("Exit");
                foreach(var p in exitPoints)
                {
                    if (p.GetComponent<PointControl>().roomId == activeRoom)
                    {
                        targetPosition = p.transform;
                        break;
                    }
                }
            }

            resetPathEvent.Invoke();
            return NodeState.FAILURE;
        }

        if (!beganChasing)
        {
            beganChasing = true;

            targetPosition = GameObject.FindGameObjectWithTag("Player").transform;
            motor.targetPosition = targetPosition.position;
            resetPathEvent.Invoke();
        }

        if (Vector3.Distance(transform.position, motor.targetPosition) > attackRange)
        {
            inRange = false;
            motor.Move();
            return NodeState.RUNNING;
        }
        else if(Physics.CheckSphere(transform.position, attackRange, playerLayer, QueryTriggerInteraction.Ignore))
        {
            inRange = true;
            Debug.Log("Caught player");
            return NodeState.SUCCESS;
        }
        else //Ako je robot na lokaciji koja je zabilježena kao meta a igra? se ne nalazi na njoj prebaci u potragu
        {
            Debug.Log("Player escaped");

            detectedPlayer = false;
            beganChasing = false;
            inRange = false;

            lostPlayerEvent.Invoke();
            return NodeState.FAILURE;
        }
    }

    public NodeState AttackPlayer()
    {
        if (killedPlayer)
            return NodeState.SUCCESS;

        if (inRange && !attacked)
        {
            StartCoroutine(DealDamage());
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }

    IEnumerator DealDamage()
    {
        attacked = true;
        var cc = FindObjectOfType<CombatController>();
        //if (cc.health <= 0)
        //    killedPlayer = true;
        //else
        cc.TakeDamage(damage);


        yield return new WaitForSeconds(delay);
        attacked = false;
    }

    void TakeDamage(Item weapon)
    {
        if (weapon.damage > 0)
        {
            health -= Mathf.Abs(weapon.damage - weapon.damage * immunities[weapon.name] / 100);
        }
    }

    public void TakePassive(string name, int damage)
    {
        if (name == "Landmine")
        {
            health-= Mathf.Abs(damage - damage * immunities[name] / 100);
            recorder.IncreaseImmunity("Landmine");
        }
    }

    public int GetImmunity(string name)
    {
        if (immunities.ContainsKey(name))
            return immunities[name];
        else return 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectableRadius);

        Vector3 viewLimit1 = DirFromAngle(-lookAngle / 2, false);
        Vector3 viewLimit2 = DirFromAngle(lookAngle / 2, false);

        Gizmos.DrawLine(transform.position, transform.position + viewLimit1 * detectableRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewLimit2 * detectableRadius);
    }

    Vector3 DirFromAngle(float angle, bool global)
    {
        if (!global)
        {
            angle += transform.eulerAngles.y;
        }
        Vector3 dir = new Vector3(Mathf.Sin(angle*Mathf.Deg2Rad), 0, Mathf.Cos(angle*Mathf.Deg2Rad));
        return dir;
    }

    public void ResetRobot()
    {
        health = 100;
        motor.moveSpeed = motor.ogMoveSpeed;
        activeRoom = 1;
        killedPlayer = false;
        beganChasing = false;
        detectedPlayer = false;
        inRange = false;
        attacked = false;
        backtracking = false;

        recorder.LoadPriorities();
        priorities = recorder.GetPriorities();
        immunities = recorder.GetImmunities();
    }
}
