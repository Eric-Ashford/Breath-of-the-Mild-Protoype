using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class cameraController : MonoBehaviour
{
    public float mouseSensitivity = 10;
    public Transform target;
    public float dstFromTarget = 2;

    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = .05f;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;

    private float yaw;
    private float pitch;

    public bool lockCursor;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
    void LateUpdate()
    {
        yaw += Input.GetAxis("Camera Horizontal") * mouseSensitivity;
        pitch -= Input.GetAxis("Camera Vertical") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

        //Vector3 targetRotation = new Vector3(pitch, yaw);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * dstFromTarget;
    }
}