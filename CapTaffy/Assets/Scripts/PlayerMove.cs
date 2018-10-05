using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(AudioSource))]

public class PlayerMove : MonoBehaviour
{
    //[SerializeField]
    //float speed;

    //Rigidbody rb;
    ////AudioSource footstep;

    //const string horizontalAxisName = "Horizontal";
    //const string verticalAxisName = "Vertical";

    //void Start()
    //{
    //    rb = this.gameObject.GetComponent<Rigidbody>();
    //    //footstep = this.gameObject.GetComponent<AudioSource>();
    //}

    //void FixedUpdate()
    //{
    //    MovePlayer();
    //    //PlayFootstep();
    //}

    //void MovePlayer()
    //{
    //    float moveHorizontal = Input.GetAxis(horizontalAxisName);
    //    float moveVertical = Input.GetAxis(verticalAxisName);

    //    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

    //    // doesn't work well with cube
    //    rb.AddForce(movement * speed);
    //}

    ////void PlayFootstep()
    ////{
    ////    if (rb.velocity.magnitude > 4.0f && !footstep.isPlaying)
    ////    {
    ////        // placeholder footstep
    ////        // TODO: need to tweak randomization/walk speed

    ////        // randomizes volume and pitch for every step, increasing with walk speed
    ////        footstep.volume = Mathf.Clamp(Random.Range(0.55f, 0.75f) * rb.velocity.magnitude, 0.0f, 1.0f);
    ////        footstep.pitch = Random.Range(0.85f, 1.15f) * (rb.velocity.magnitude / 6);

    ////        footstep.Play();
    ////    }
    ////}
    ///

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

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        cc = this.gameObject.GetComponent<CapsuleCollider>();
        cameraTransform = Camera.main.transform;

        isOnGround = true;
    }

    void Update()
    {
        HandleFriction();
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis(horizontalAxisName);
        verticalInput = Input.GetAxis(verticalAxisName);
        isJumping = Input.GetButtonDown(jumpButtonName);

        /*
            This if statment is how tolerant we are on changing the direction based on where the camera is looking.
            For example, if the player is moving to the left/right of where the camera is looking and then he rotates the camera
            so it looks towards where he is going, we will keep moving at the same direction as before
        */
        storDir = cameraTransform.right;        //This means, the player can keep moving in the same direction they were before even if they change the camera angle


        if (isOnGround)        //Jump!, does not rotate
        {
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

            //Jump controls
            if (isJumping && isOnGround)
            {
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);      //ForceMode.Impulse (I think) gives all the force to the jump for only one frame.
            }
        }

        /*Rotates the Character*/
        //Find a position in front of where the camera is looking
        facingDirection = transform.position + (storDir * horizontalInput) + (cameraTransform.forward * verticalInput);
        //Find the direction from that position
        Vector3 dir = facingDirection - transform.position;
        dir.y = 0;  //The player should not be bouncing up and down.

        //If the player has been given input, we move!
        if (horizontalInput != 0 || verticalInput != 0)
        {
            //find the angle, between the character's rotation and where the camera is looking
            float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(dir));

            //and if it's not zero (to avoid a warning)
            if (angle != 0)   //look towards the camera
            { rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime); }
        }
    }

    //If we are touching something (like the ground )
    void OnCollisionEnter(Collision other)
    {
        //This means we are on the ground

        if (other.gameObject.tag == "Ground")
        {
            isOnGround = true;
            rb.drag = 5;
        }
    }

    //Once we are no longer touching the object we collided with earlier
    void OnCollisionExit(Collision other)
    {
        //We want to know when we have left the ground (or anything else)
        if (other.gameObject.tag == "Ground")    //You can copy this if statement and make it "Vehicle" or something to jump off a car.
        {
            isOnGround = false;
            rb.drag = 0;
        }
    }

    void HandleFriction()
    {
        if (horizontalInput == 0 && verticalInput == 0)       // max friction when not moving
        {
            cc.material = maxFriction;
        }
        else
        {
            cc.material = zeroFriction;     // zero friction when moving
        }
    }
}

// copied a lot from: https://www.reddit.com/r/Unity3D/comments/4zs2zy/pffft_who_needs_a_third_person_controller/