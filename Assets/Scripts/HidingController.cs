using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HidingController : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera freeLookCam, hidingCamera;
    //[HideInInspector]    
    public bool inRange, hiding;
    public int range;
    public LayerMask hidingMask;
    [HideInInspector]
    public string hidingSpot;

    public GameObject hideText, leaveText;
    PlayerTriggers playerTriggers;

    private void OnEnable()
    {
        playerTriggers = new PlayerTriggers();
        playerTriggers.Enable();
        playerTriggers.DummyPlayer.HideTest.performed += ctx => Hide();
    }

    private void OnDisable()
    {
        playerTriggers.DummyPlayer.HideTest.performed -= ctx => Hide();
    }

    void Update()
    {
        if (!hiding)
        {
            CheckAvailability();
        }
        else if(!leaveText.activeSelf)
        {
            hideText.SetActive(false);
            leaveText.SetActive(true);
        }
    }

    bool CheckAvailability()
    {
        if (inRange)
        {
            var direction = freeLookCam.State.CorrectedOrientation * Vector3.forward;

            Debug.DrawRay(transform.position, direction, Color.red);
            if (Physics.Raycast(freeLookCam.transform.position, direction, range, hidingMask, QueryTriggerInteraction.Ignore))
            {
                if (!hideText.activeSelf)
                {
                    hideText.SetActive(true);
                }
                return true;
            }
            else
            {
                if (hideText.activeSelf)
                {
                    hideText.SetActive(false);
                }
            }
        }
        else if (hideText.activeSelf)
        {
            hideText.SetActive(false);
        }

        return false;
    }

    void Hide()
    {
        if (hiding)
        {
            hiding = false;
            leaveText.SetActive(false);

            TogglePlayer(true);

            Debug.Log("Player left the hiding spot");
        }
        else if (CheckAvailability())
        {
            TogglePlayer(false);
            hiding = true;
            Debug.Log("Player hid himself");
        }
    }

    void TogglePlayer(bool status)
    {
        GetComponent<FirstPersonController>().enabled = status;
        GetComponent<PlayerInput>().enabled = status;
        GetComponent<CharacterController>().enabled = status;
        hidingCamera.Priority = status?0:20;
    }
}
