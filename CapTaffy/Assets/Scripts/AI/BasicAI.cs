using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{

    [SerializeField]
    private float playerDistance = 0f,
        lookDistance = 80f,
        chaseDistance = 50f,
        rangedDistance = 20f,
        meleeDistance = 10f,
        movementSpeed = 40f,
        turnSpeed = 1f,
        rangeAttackDelay = 0f,
        meleeAttackDelay = 0f;
    [SerializeField]
    private Transform player;

    private Rigidbody rb;
    private Animator anim;
    private Vector3 lookDirection;
    private bool waitActive;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        anim.SetFloat("speed", rb.velocity.magnitude);
        playerDistance = Vector3.Distance(player.position, transform.position);

        DecideAction();        
    }

    private void DecideAction()
    {
        if (playerDistance <= meleeDistance)
        {
            if (!waitActive)
            {
                MeleeAttack();
                StartCoroutine(Wait(meleeAttackDelay));
                AlignToPlayer();
            }
        }
        else if (playerDistance <= rangedDistance)
        {
            if (!waitActive)
            {
                ShootFire();
                StartCoroutine(Wait(rangeAttackDelay));
                AlignToPlayer();
            }
        }
        else if (playerDistance <= chaseDistance)
        {
            if (!waitActive)
            {
                AlignToPlayer();
                MoveTowardsPlayer();
            }
        }
        else if (playerDistance <= lookDistance)
        {
            anim.SetBool("chasePlayer", false);
            anim.SetBool("attackPlayer", false);
            anim.SetBool("breatheFire", false);
            if (!waitActive)
            {
                AlignToPlayer();
            }
        }
        else
        {
            anim.SetBool("chasePlayer", false);
            anim.SetBool("attackPlayer", false);
            anim.SetBool("breatheFire", false);
        }
    }

    private void AlignToPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0f, rotation.eulerAngles.y, 0f));
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), turnSpeed * Time.deltaTime);
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

    private void RemoveCreature()
    {
        Destroy(gameObject);
    }

    private void ResetIdle()
    {
        anim.SetTrigger("resetIdle");
    }

    private IEnumerator Wait(float delay)
    {
        waitActive = true;
        yield return new WaitForSeconds(delay);
        waitActive = false;
    }
}