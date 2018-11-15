using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{

    [SerializeField]
    private float playerDistance,
        lookDistance,
        attackDistance,
        movementSpeed,
        damping;
    [SerializeField]
    private Transform player;
    //lava bug's animator
    public Animator lavaBug;
    private Rigidbody rb;
    private Renderer rndr;

    // Use this for initialization
    void Start()
    {
        lavaBug = GetComponent<Animator>();
        rndr = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        playerDistance = Vector3.Distance(player.position, transform.position);

        if (playerDistance <= lookDistance)
        {
            rndr.material.color = Color.yellow;
                
            AlignToPlayer();
        }

        if (playerDistance <= attackDistance)
        {
            rndr.material.color = Color.red;
       
            MoveTowardsPlayer();
        }

        if (playerDistance > lookDistance && playerDistance > attackDistance)
        {
            rndr.material.color = Color.white;
       
        }
    }

    private void AlignToPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0f, rotation.eulerAngles.y, 0f));
    }

    private void MoveTowardsPlayer()
    {
        rb.AddForce(transform.forward * movementSpeed, ForceMode.Impulse);
    }

    //Don't know why this doesn't work

    //private void Attack()
    //{
    //    rb.AddForce(transform.forward * movementSpeed, ForceMode.Force);
        
    //}
}