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
    [HideInInspector]
    public Vector3 targetPosition, targetRotation;
    [HideInInspector]
    public bool inRange, hiding;
    public int range;
    public LayerMask hidingMask;
    [HideInInspector]
    public string hidingSpot;
    Transform spotTransform;
    public bool sneaking, moving;
    [HideInInspector]
    public bool backwards;
    [HideInInspector]
    public Vector2 movementVector;

    public GameObject hideText, leaveText;
    PlayerTriggers playerTriggers;

    public int currentRoom = 2;
    bool alreadyFreed = false;

    public Transform ventVisual, closetVisual;
    Quaternion ventRotationOG, closetRotationOG;
    public float distanceFromCamera = 1f;

    public delegate void HideDelegate(string tag);
    public event HideDelegate hideEvent;

    private void OnEnable()
    {
        playerTriggers = new PlayerTriggers();
        playerTriggers.Enable();
        playerTriggers.DummyPlayer.HideTest.performed += ctx => Hide();

        playerTriggers.DummyPlayer.Sneak.started += ctx => Sneak(true);
        playerTriggers.DummyPlayer.Sneak.canceled += ctx => Sneak(false);

        playerTriggers.DummyPlayer.MoveDetector.started += ctx => Moving(true);
        playerTriggers.DummyPlayer.MoveDetector.canceled += ctx => Moving(false);
        playerTriggers.DummyPlayer.GoingBackwards.started += ctx => Backwards(true);
        playerTriggers.DummyPlayer.GoingBackwards.canceled += ctx => Backwards(false);


        ventRotationOG = ventVisual.rotation;
        closetRotationOG = closetVisual.rotation;
        ventVisual.gameObject.SetActive(false);
        closetVisual.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        playerTriggers.DummyPlayer.HideTest.performed -= ctx => Hide();

        playerTriggers.DummyPlayer.Sneak.started -= ctx => Sneak(true);
        playerTriggers.DummyPlayer.Sneak.canceled -= ctx => Sneak(false);

        playerTriggers.DummyPlayer.MoveDetector.started -= ctx => Moving(true);
        playerTriggers.DummyPlayer.MoveDetector.canceled -= ctx => Moving(false);

        playerTriggers.DummyPlayer.GoingBackwards.started -= ctx => Backwards(true);
        playerTriggers.DummyPlayer.GoingBackwards.canceled -= ctx => Backwards(false);
    }

    void Update()
    {
        movementVector = playerTriggers.DummyPlayer.MoveDetector.ReadValue<Vector2>();
        Debug.Log(movementVector);

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

            RaycastHit hitInfo;
            if (Physics.Raycast(freeLookCam.transform.position, direction, out hitInfo, range , hidingMask, QueryTriggerInteraction.Ignore))
            {
                spotTransform = hitInfo.transform;

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
            hideEvent.Invoke(spotTransform.tag);
            hiding = false;
            leaveText.SetActive(false);
            sneaking = true;

            TogglePlayer(true);
            TurnOffVisuals();
            Debug.Log("Player left the hiding spot");

            StartCoroutine(KeepSafe());
        }
        else if (CheckAvailability())
        {
            TogglePlayer(false);
            SetupCamera();
            hiding = true;
            alreadyFreed = false;
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

    void SetupCamera()
    {
        hidingCamera.transform.position = spotTransform.position;
        hidingCamera.transform.rotation = Quaternion.LookRotation(spotTransform.forward);

        if (spotTransform.tag == "Vent")
        {
            ventVisual.position = spotTransform.position + spotTransform.forward * distanceFromCamera;
            ventVisual.rotation = Quaternion.LookRotation(Vector3.up, spotTransform.forward);
            ventVisual.gameObject.SetActive(true);
        }
        else
        {
            closetVisual.position = spotTransform.position + spotTransform.forward * distanceFromCamera;
            closetVisual.rotation=Quaternion.LookRotation(Vector3.up, spotTransform.forward);
            closetVisual.gameObject.SetActive(true);
        }
    }

    void TurnOffVisuals()
    {
        if (ventVisual.gameObject.activeSelf)
        {
            ventVisual.rotation = ventRotationOG;
            ventVisual.gameObject.SetActive(false);
        }
        if (closetVisual.gameObject.activeSelf)
        {
            closetVisual.rotation = closetRotationOG;
            closetVisual.gameObject.SetActive(false);
        }
    }

    void Sneak(bool toggle)
    {
        if (!alreadyFreed)
            alreadyFreed = true;

        sneaking = toggle;
    }

    void Moving(bool toggle)
    {
        moving = toggle;
        
    }

    void Backwards(bool toggle)
    {
        backwards = toggle;
    }

    IEnumerator KeepSafe()
    {
        yield return new WaitForSeconds(3);

        if (!alreadyFreed)
        {
            sneaking = false;
            alreadyFreed = true;
        }
    }

    public void ResetPlayer()
    {
        currentRoom = 1;
        hiding = false;
        TurnOffVisuals();
        TogglePlayer(true);
        hidingSpot = "";
        spotTransform = null;
    }
}
