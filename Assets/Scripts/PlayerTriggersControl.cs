using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggersControl : MonoBehaviour
{
    public PlayerTriggers triggers;
    public LayerMask pointsLayer;

    private void Start()
    {
        triggers = new PlayerTriggers();
        triggers.Enable();

        triggers.DummyPlayer.HideTest.performed += ctx => Hide();
        triggers.DummyPlayer.HideTest.Enable();
    }
   
    public void Hide()
    {
        
    }
}
