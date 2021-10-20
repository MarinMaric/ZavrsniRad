using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointControl : MonoBehaviour
{
    public Vector3 location;
    public bool visited;

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
