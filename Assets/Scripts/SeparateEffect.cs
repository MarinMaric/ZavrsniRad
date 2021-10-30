using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeparateEffect : MonoBehaviour
{
    public delegate void Activate();
    public Activate activateFunction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Robot")
        {
            activateFunction();
        }
    }
}
