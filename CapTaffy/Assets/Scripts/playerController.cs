using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEditor;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [SerializeField] float walkSpeed = 6f;
    [SerializeField] float runSpeed = 15f;

    [SerializeField] float turnSmoothTime = .2f;
    private float turnSmoothVelocity;

    Transform cameraM;


    // Use this for initialization
    void Start()
    {
        cameraM = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraM.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        bool running = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float speed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;                                                         // If were running then runspeed else walkspeed

        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

    }
}