using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Vector3 targetPosition;
    bool detectedPlayer;
    public float selectableRadius, detectableRadius, speed, lookAngle;
    public LayerMask pointsLayer, playerLayer;
    public Recorder recorder;
    List<string> priorities;
    List<Vector3> points = new List<Vector3>();
    int priorityIndex = -1;

    void Start()
    {
        targetPosition = transform.position;
        priorities = recorder.GetPriorities();
    }
    
    void Update()
    {
    }

    public NodeState SelectPoint()
    {
        if (transform.position == targetPosition && !detectedPlayer)
        {
            while (points.Count == 0 && priorityIndex<priorities.Count)
            {
                priorityIndex++;
                var hits = Physics.SphereCastAll(transform.position, selectableRadius, transform.forward, selectableRadius, pointsLayer.value, QueryTriggerInteraction.Collide);
                foreach (var h in hits)
                {
                    if (h.collider.tag == priorities[priorityIndex])
                        points.Add(h.collider.transform.position);
                }
            }

            //for now it's just going in circles randomly but later it needs to differentiate
            //between visited points and those left unchecked
            targetPosition = points[(int)Random.Range(0, points.Count)];
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
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
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
