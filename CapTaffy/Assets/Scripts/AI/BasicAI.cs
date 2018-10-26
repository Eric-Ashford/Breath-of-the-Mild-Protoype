using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{

    [SerializeField]
    private float creatureHealth = 100f,
        playerDistance = 0f,
        lookDistance = 80f,
        chaseDistance = 50f,
        rangedDistance = 20f,
        meleeDistance = 10f,
        movementSpeed = 40f;
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

        if (playerDistance <= meleeDistance)
        {
            MeleeAttack();
        }
        else if (playerDistance <= rangedDistance)
        {
            ShootFire();
        }
        else if (playerDistance <= chaseDistance)
        {
            AlignToPlayer();
            MoveTowardsPlayer();
        }
        else
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
        anim.SetBool("breatheFire", false);
        anim.SetBool("chasePlayer", true);
        rb.AddForce(transform.forward * movementSpeed, ForceMode.Acceleration);
    }

    private void ShootFire()
    {       
        anim.SetBool("chasePlayer", false);
        anim.SetBool("attackPlayer", false);
        anim.SetBool("breatheFire", true);
    }
    
    private void MeleeAttack()
    {
        anim.SetBool("breatheFire", false);
        anim.SetBool("attackPlayer", true);
    }

    private void CheckHealth()
    {
        if (creatureHealth <= 0)
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("isDead", true);
            anim.SetBool("chasePlayer", false);
            anim.SetBool("breatheFire", false);
            anim.SetBool("attackPlayer", false);
        }
    }

    private void RemoveCreature()
    {
        Destroy(gameObject);
    }

    private void ResetIdle()
    {
        anim.SetTrigger("resetIdle");
    }
}