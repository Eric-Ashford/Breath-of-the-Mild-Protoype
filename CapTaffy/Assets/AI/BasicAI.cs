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

    private Rigidbody rb;
    private Renderer rndr;

    // Use this for initialization
    void Start()
    {
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
            Attack();
        }
    }

    private void AlignToPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    private void Attack()
    {
        rb.AddForce(transform.forward * movementSpeed * Time.deltaTime);
    }
}