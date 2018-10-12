using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    float walkSpeed = 5.0f;
    [SerializeField]
    float runSpeed = 25.0f;
    [SerializeField]
    float turnSpeed = 5.0f;
    [SerializeField]
    float jumpStrength = 5.0f;

    [SerializeField]
    PhysicMaterial zeroFriction;
    [SerializeField]
    PhysicMaterial maxFriction;

    [SerializeField]
    AudioClip[] footstepsArray;

    Rigidbody rb;
    CapsuleCollider cc;
    AudioSource footstep;
    Transform cameraTransform;

    Vector3 facingDirection;
    Vector3 previousDirection;

    float horizontalInput;
    float verticalInput;
    float moveSpeed;

    bool isRunning;
    bool isJumping;
    bool isOnGround;
    bool isTakingStep;

    const string horizontalAxisName = "Horizontal";
    const string verticalAxisName = "Vertical";
    const string jumpButtonName = "Jump";
    const string sprintButtonName = "Sprint";

    void Awake()
    {
        isRunning = false;
        isJumping = false;
        isOnGround = true;
        isTakingStep = false;
    }

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        cc = this.gameObject.GetComponent<CapsuleCollider>();
        footstep = this.gameObject.GetComponent<AudioSource>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        ChangeFrictionMaterial();
    }

    void FixedUpdate()
    {
        MovePlayer();
        PlayFootstep();
    }

    void ChangeFrictionMaterial()
    {
        if (horizontalInput == 0.0f && verticalInput == 0.0f)
        {
            cc.material = maxFriction;      //a lot of friction when wanting to stop so player doesn't slide forever
            Debug.Log("reached max frictipnm.");
        }
        else
        {
            cc.material = zeroFriction;     //zero friction when wanting to move so player actually moves
            Debug.Log("reached zero frictipnm.");
        }
    }

    void MovePlayer()
    {
        horizontalInput = Input.GetAxis(horizontalAxisName);
        verticalInput = Input.GetAxis(verticalAxisName);
        isJumping = Input.GetButtonDown(jumpButtonName);
        
        previousDirection = cameraTransform.right;

        if (isOnGround)
        {
            //movement controls
            if (Input.GetButton(sprintButtonName) || Input.GetAxis(sprintButtonName) > 0.0f)
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

            rb.AddForce(((previousDirection * horizontalInput) + (cameraTransform.forward * verticalInput)) * moveSpeed / Time.deltaTime);

            //jump controls
            if (isJumping && isOnGround)
            {
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);

                // TODO: play jump sound
            }
        }

        //rotation controls
        facingDirection = transform.position + (previousDirection * horizontalInput) + (cameraTransform.forward * verticalInput);
        Vector3 dir = facingDirection - this.gameObject.transform.position;
        dir.y = 0;  //stops player from tipping over

        if (horizontalInput != 0.0f || verticalInput != 0.0f)     //only rotate player when moving
        {
            float angle = Quaternion.Angle(this.gameObject.transform.rotation, Quaternion.LookRotation(dir));

            if (angle != 0.0f)
            {
                rb.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);       //rotate player to face look direction
            }
        }
    }

    void PlayFootstep()
    {
        if (rb.velocity.magnitude > 2.0f && !footstep.isPlaying && !isTakingStep && isOnGround)
        {
            // choose random clip, excluding the first one
            int n = Random.Range(1, footstepsArray.Length - 1);
            footstep.clip = footstepsArray[n];

            // randomizes volume and pitch for every step, increasing with walk speed
            footstep.volume = Mathf.Clamp(Random.Range(0.4f, 0.65f) * rb.velocity.magnitude, 0.0f, 1.0f);
            footstep.pitch = Random.Range(0.9f, 1.1f) * (Mathf.Clamp(rb.velocity.magnitude / 7, 1.0f, 1.5f));

            footstep.Play();

            // move clip to first index so it won't play again right away
            footstepsArray[n] = footstepsArray[0];
            footstepsArray[0] = footstep.clip;

            StartCoroutine(TakeStep());
        }
    }

    IEnumerator TakeStep()
    {
        isTakingStep = true;

        // pause in between playing footstep sound, decreasing with walk speed
        yield return new WaitForSecondsRealtime(0.5f / (Mathf.Clamp(rb.velocity.magnitude / 7.0f, 0.8f, 1.5f)));

        isTakingStep = false;
    }

    void OnCollisionEnter(Collision other)
    {
        //check if on the ground
        if (other.gameObject.tag == "Ground")       //need to use ground tag for any walkable surface
        {
            isOnGround = true;
            rb.drag = 5.0f;        //increase drag when on ground
        }
    }
    
    void OnCollisionExit(Collision other)
    {
        //check if left the ground
        if (other.gameObject.tag == "Ground")
        {
            isOnGround = false;
            rb.drag = 0.0f;        //decrease drag when in the air
        }
    }
}