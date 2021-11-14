using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public delegate void VictoryDelegate();
    public event VictoryDelegate victoryEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            victoryEvent.Invoke();
        }
    }
}
