using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    List<string> priorities;
    List<PointControl> points = new List<PointControl>();
    int priorityIndex = -1;
    public int activeRoom = 1;

    public delegate void ResetPath();
    public event ResetPath resetPathEvent;
    List<int> checkedRooms;

    void Start()
    {
        targetPosition = transform;
        motor.targetPosition = transform.position;
        priorities = recorder.GetPriorities();

        checkedRooms = new List<int>();
    }
    
    public NodeState SelectPoint()
    {
        if (transform.position == motor.targetPosition && !detectedPlayer)
        {
            if (points.Count == 0 && priorityIndex+1 < priorities.Count)
            {
                priorityIndex++;
            }

            //If point is reached and no player has been found, go to random unvisited point with said tag
            var hits = Physics.SphereCastAll(transform.position, selectableRadius, transform.forward, selectableRadius, pointsLayer.value, QueryTriggerInteraction.Collide);
            points.Clear();
            foreach (var h in hits)
            {
                if (h.collider.tag == priorities[priorityIndex] && h.collider.GetComponent<PointControl>().visited == false && h.collider.GetComponent<PointControl>().roomId==activeRoom)
                    points.Add(h.collider.GetComponent<PointControl>());
            }

            //for now it's just going in circles randomly but later it needs to differentiate
            //between visited points and those left unchecked

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
            else
            {
                //if all the points in the room have been checked then mark the whole room as checked
                checkedRooms.Add(activeRoom);

                //prepare priority for next room
                priorityIndex = 0;

                //move on to next room (later I can make it go back as well)
                foreach (var h in hits)
                {
                    h.collider.GetComponent<PointControl>().visited = false;
                    if (h.collider.tag == "Exit" && h.collider.GetComponent<PointControl>().roomId==activeRoom && h.collider.GetComponent<PointControl>().visited == false)
                        points.Add(h.collider.GetComponent<PointControl>());
                }

                if (points.Count > 0)
                {
                    targetPosition = points[0].transform;
                    resetPathEvent.Invoke();
                }
            }
        }

        return NodeState.SUCCESS;
    }

    public NodeState MoveToPoint()
    {
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
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, detectableRadius, transform.forward, 0, playerLayer.value, QueryTriggerInteraction.Collide);

        foreach(RaycastHit hit in hits)
        {
            Vector3 direction = (hit.transform.position - transform.position).normalized;
            float targetAngle = Vector3.Angle(transform.forward, direction);
            if (targetAngle < lookAngle / 2)
            {
                if(!Physics.Raycast(transform.position, hit.transform.position, detectableRadius, obstacleLayer))
                    detectedPlayer = true;
            }
        }
        
        if (detectedPlayer)
            return NodeState.SUCCESS;
        else
            return NodeState.FAILURE;
    }

    public NodeState ChasePlayer()
    {
        if (!beganChasing)
        {
            beganChasing = true;

            targetPosition = GameObject.FindGameObjectWithTag("Player").transform;
            motor.targetPosition = targetPosition.position;
            resetPathEvent.Invoke();
        }

        return NodeState.RUNNING;
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
}
