using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    RobotMotor motor;
    Vector3 targetPosition;
    bool detectedPlayer;
    public float selectableRadius, detectableRadius, lookAngle;
    public LayerMask pointsLayer, playerLayer, obstacleLayer;
    public Recorder recorder;
    List<string> priorities;
    List<PointControl> points = new List<PointControl>();
    int priorityIndex = -1;

    void Start()
    {
        targetPosition = transform.position;
        priorities = recorder.GetPriorities();
    }
    
    public NodeState SelectPoint()
    {
        if (transform.position == targetPosition && !detectedPlayer)
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
                if (h.collider.tag == priorities[priorityIndex] && h.collider.GetComponent<PointControl>().visited == false)
                    points.Add(h.collider.GetComponent<PointControl>());
            }

            //for now it's just going in circles randomly but later it needs to differentiate
            //between visited points and those left unchecked

            if (points.Count > 0)
            {
                int index = (int)Random.Range(0, points.Count);
                targetPosition = points[index].location;
                points.RemoveAt(index);
            }
        }



        return NodeState.SUCCESS;

        #region oldcode
        /*if (transform.position==targetPosition && !detectedPlayer)
        {
            //Select random point from the ones nearby in a selectable radius
            List<Vector3> points = new List<Vector3>();
            var hits = Physics.SphereCastAll(transform.position, selectableRadius, transform.forward, selectableRadius, pointsLayer.value, QueryTriggerInteraction.Collide);
            foreach (var h in hits)
            {
                points.Add(h.transform.position);
            }

            targetPosition = points[(int)Random.Range(0, points.Count)];
        
        }   
        */
        #endregion
    }

    public NodeState MoveToPoint()
    {
        if (transform.position != targetPosition && !detectedPlayer)
        {
            motor.Move(targetPosition);
            return NodeState.RUNNING;
        }
        else if(transform.position == targetPosition)
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

    public NodeState PrintMessage()
    {
        Debug.Log("Printed message");
        return NodeState.SUCCESS;
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
