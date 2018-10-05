using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(AudioSource))]

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    float walkSpeed = 5f;
    [SerializeField]
    float runSpeed = 25f;
    [SerializeField]
    float turnSpeed = 5;
    [SerializeField]
    float jumpStrength = 5;

    [SerializeField]
    PhysicMaterial zeroFriction;
    [SerializeField]
    PhysicMaterial maxFriction;

    Rigidbody rb;
    CapsuleCollider cc;
    //AudioSource footstep;
    Transform cameraTransform;

    Vector3 facingDirection;
    Vector3 storDir;

    float horizontalInput;
    float verticalInput;
    float moveSpeed;

    bool isRunning;
    bool isJumping;
    bool isOnGround;

    const string horizontalAxisName = "Horizontal";
    const string verticalAxisName = "Vertical";
    const string jumpButtonName = "Jump";

    void Awake()
    {
        isRunning = false;
        isJumping = false;
        isOnGround = true;
    }

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        cc = this.gameObject.GetComponent<CapsuleCollider>();
        //footstep = this.gameObject.GetComponent<AudioSource>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        ChangeFrictionMaterial();
    }

    void FixedUpdate()
    {
        MovePlayer();
        //PlayFootstep();
    }

    void ChangeFrictionMaterial()
    {
        if (horizontalInput == 0 && verticalInput == 0)
        {
            cc.material = maxFriction;      //a lot of friction when wanting to stop so player doesn't slide forever
        }
        else
        {
            cc.material = zeroFriction;     //zero friction when wanting to move so player actually moves
        }
    }

    void MovePlayer()
    {
        horizontalInput = Input.GetAxis(horizontalAxisName);
        verticalInput = Input.GetAxis(verticalAxisName);
        isJumping = Input.GetButtonDown(jumpButtonName);
        
        storDir = cameraTransform.right;

        if (isOnGround)
        {
            //movement controls
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }

            if (isRunning)
            {
                moveSpeed = runSpeed;
            }
            else
            {
                moveSpeed = walkSpeed;
            }

            rb.AddForce(((storDir * horizontalInput) + (cameraTransform.forward * verticalInput)) * moveSpeed / Time.deltaTime);

            //jump controls
            if (isJumping && isOnGround)
            {
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);

                // TODO: play jump sound
            }
        }

        //rotation controls
        facingDirection = transform.position + (storDir * horizontalInput) + (cameraTransform.forward * verticalInput);
        Vector3 dir = facingDirection - this.gameObject.transform.position;
        dir.y = 0;  //stops player from tipping over

        if (horizontalInput != 0 || verticalInput != 0)     //only rotate player when moving
        {
            float angle = Quaternion.Angle(this.gameObject.transform.rotation, Quaternion.LookRotation(dir));

            if (angle != 0)
            {
                rb.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);       //rotate player to face look direction
            }
        }
    }

    // TODO: change to use footstep array code
    //void PlayFootstep()
    //{
    //    if (rb.velocity.magnitude > 4.0f && !footstep.isPlaying)
    //    {
    //        // placeholder footstep
    //        // TODO: need to tweak randomization/walk speed

    //        // randomizes volume and pitch for every step, increasing with walk speed
    //        footstep.volume = Mathf.Clamp(Random.Range(0.55f, 0.75f) * rb.velocity.magnitude, 0.0f, 1.0f);
    //        footstep.pitch = Random.Range(0.85f, 1.15f) * (rb.velocity.magnitude / 6);

    //        footstep.Play();
    //    }
    //}

    void OnCollisionEnter(Collision other)
    {
        //check if on the ground
        if (other.gameObject.tag == "Ground")       //need to use ground tag for any jumpable surface
        {
            isOnGround = true;
            rb.drag = 5;        //increase drag when on ground
        }
    }
    
    void OnCollisionExit(Collision other)
    {
        //check if left the ground
        if (other.gameObject.tag == "Ground")
        {
            isOnGround = false;
            rb.drag = 0;        //decrease drag when in the air
        }
    }
}