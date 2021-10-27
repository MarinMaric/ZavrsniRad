using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointControl : MonoBehaviour
{
    [HideInInspector]
    public Vector3 location;
    public bool visited;
    public int roomId;

    private void Awake()
    {
        location = transform.position;
    }

    private void Start()
    {
        FindObjectOfType<EnemyController>().resetPathEvent += ResetPoint;
        if(tag!="Exit")
            FindObjectOfType<EnemyController>().lostPlayerEvent += ResetPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Robot")
        {
            if (transform.tag == "Exit")
            {
                Debug.Log("Marked as visited");
            }
            visited = true;
        }
    }

    void ResetPoint()
    {
        if (roomId != FindObjectOfType<EnemyController>().activeRoom)
        {
            if (transform.tag == "Exit")
            {
                Debug.Log("Freed up");
            }
            visited = false;
        }
    }
}
