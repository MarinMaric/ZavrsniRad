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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Robot")
            visited = true;
    }
}
