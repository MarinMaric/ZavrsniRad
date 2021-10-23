using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMotor : MonoBehaviour
{
    public float moveSpeed=5f;
    public Vector3 targetPosition;
    public List<Vector3> waypoints;
    Vector3 currentWaypoint;
    int index;

    private void Update()
    {
        if (transform.position == currentWaypoint && index < waypoints.Count-1)
        {
            index++;
            currentWaypoint = waypoints[index];
        }
    }

    public void Move()
    {
        Rotate(currentWaypoint);
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, Time.deltaTime * moveSpeed);
        Debug.Log(currentWaypoint);
    }

    public void Rotate(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.normalized.x, 0, direction.normalized.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
    }

    public void ChangeWaypoints(List<Vector3> newWaypoints)
    {
        waypoints = newWaypoints;
        index = 0;
        targetPosition = waypoints[waypoints.Count-1];
        currentWaypoint = waypoints[0];
    }
}
