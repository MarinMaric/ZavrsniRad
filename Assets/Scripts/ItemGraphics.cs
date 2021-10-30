using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGraphics : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera playerCam;

    void Update()
    {
        Vector3 direction = playerCam.State.CorrectedOrientation*Vector3.forward;
        var cameraRotation = playerCam.State.CorrectedOrientation;
        transform.rotation = Quaternion.Euler(Mathf.Clamp(cameraRotation.eulerAngles.x, 30, cameraRotation.eulerAngles.x), cameraRotation.eulerAngles.y, Mathf.Clamp(cameraRotation.eulerAngles.z, 30, float.MaxValue));
    }
}
