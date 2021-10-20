using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggersControl : MonoBehaviour
{
    public delegate void HideDelegate(string tag);
    public event HideDelegate hideEvent;
    public PlayerTriggers triggers;

    private void Start()
    {
        triggers = new PlayerTriggers();
        triggers.Enable();

        triggers.DummyPlayer.HideTest.performed += ctx => Hide();
        triggers.DummyPlayer.HideTest.Enable();
    }

    public void Hide()
    {
        if (hideEvent != null)
        {
            hideEvent.Invoke("Vent");
        }
    }
}
