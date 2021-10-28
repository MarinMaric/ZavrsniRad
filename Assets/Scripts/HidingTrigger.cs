using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingTrigger : MonoBehaviour
{
    public string hidingSpot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<HidingController>().inRange = true;
            FindObjectOfType<HidingController>().hidingSpot = hidingSpot;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<HidingController>().inRange = false;
            FindObjectOfType<HidingController>().hidingSpot = "";
        }
    }
}
