using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{

    [SerializeField]
    private float creatureHealth = 100f,
        playerDistance = 0f,
        lookDistance = 20f,
        chaseDistance = 15f,
        attackDistance = 10f,
        movementSpeed = 40f,
        damping = 10f;
    [SerializeField]
    private Transform player;
    
    private Rigidbody rb;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckHealth();
    }

    private void FixedUpdate()
    {
        anim.SetFloat("speed", rb.velocity.magnitude);
        playerDistance = Vector3.Distance(player.position, transform.position);

        if (playerDistance <= lookDistance)
        {
            AlignToPlayer();
        }

        if (playerDistance <= attackDistance)
        {
            AttackPlayer();
        }
        else if (playerDistance <= chaseDistance)
        {
            AlignToPlayer();
            MoveTowardsPlayer();           
        }
        else
        {
            rb.AddForce(-transform.forward * movementSpeed, ForceMode.Force);
            anim.SetBool("chasePlayer", false);
        }

        if (playerDistance > lookDistance && playerDistance > chaseDistance)
        {
            anim.SetBool("chasePlayer", false);
        }
    }

    private void AlignToPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0f, rotation.eulerAngles.y, 0f));
    }   

    private void MoveTowardsPlayer()
    {
        anim.SetBool("attackPlayer", false);
        anim.SetBool("chasePlayer", true);
        rb.AddForce(transform.forward * movementSpeed, ForceMode.Acceleration);
    }

    private void AttackPlayer()
    {
        rb.AddForce(-transform.forward * movementSpeed, ForceMode.Force);
        anim.SetBool("chasePlayer", false);
        anim.SetBool("attackPlayer", true);
    }

    private void CheckHealth()
    {
        if (creatureHealth <= 0)
        {
            rb.AddForce(-transform.forward * movementSpeed, ForceMode.Force);
            anim.SetBool("isDead", true);
            anim.SetBool("chasePlayer", false);
            anim.SetBool("attackPlayer", false);
        }
    }

    private void RemoveCreature()
    {
        Destroy(gameObject);
    }
}