using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    [SerializeField]
    private float CameraMoveSpeed = 120.0f;

    public GameObject CameraFollowObj;

    private Vector3 FollowPOS;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;
    public GameObject CameraObj;
    public GameObject PlayerObj;
    private static float offsetX;
    private static float offsetY;
    private static float offsetZ;

    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;

    public float smoothX;
    public float smoothY;
    private float rotX = 0.0f;
    private float rotY = 0.0f;

    [SerializeField]
    Vector3 playerOffset = new Vector3(offsetX, offsetY, offsetZ);

    // Use this for initialization
    void Start()
    {
        //Get the local rotation of the Camera, grab the euler angles and set our rotation axis variables to the local rotation's axis.
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotY = rotation.y;
        rotX = rotation.x;

        //Locking the cursor, meaning to not show it during play
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {

        float inputX = Input.GetAxis("RightAxis X");
        float inputZ = Input.GetAxis("RightAxis Y");

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        finalInputX = inputX + mouseX;
        finalInputZ = inputZ + mouseY;

        //Rotate on input
        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        rotX += finalInputZ * inputSensitivity * Time.deltaTime;

        //Clamping the value of X so it cant go higher or lower then what we set
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);


        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    //Happens after each frame
    void LateUpdate()
    {
        cameraUpdate();
    }

    void cameraUpdate()
    {
        //Set the target object ot follow
        Transform target = CameraFollowObj.transform;

        //Move towards game object that is the target
        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, (target.position - (transform.forward * playerOffset.z) + (transform.up * playerOffset.y) + (transform.right * playerOffset.x)), step);


    }
}
