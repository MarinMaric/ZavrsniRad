using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMotor : MonoBehaviour
{
    public float moveSpeed=5f;

    public void Move(Vector3 targetPosition)
    {
        Rotate(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    public void Rotate(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.normalized.x, 0, direction.normalized.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
    }
}
