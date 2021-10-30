using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    public Item item;
    public Transform graphics;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<CombatController>().showText = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<CombatController>().showText = false; 
        }
    }
}

public enum ItemType
{
    Firearm, Passive, Throwable, Heal
}
