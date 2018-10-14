using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{

    [SerializeField]
    private float creatureHealth,
        playerDistance,
        lookDistance,
        chaseDistance,
        attackDistance,
        movementSpeed,
        damping;
    [SerializeField]
    private Transform player;
    
    private Rigidbody rb;
    private Animator anmtr;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anmtr = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckHealth();
    }

    private void FixedUpdate()
    {
        playerDistance = Vector3.Distance(player.position, transform.position);

        if (playerDistance <= lookDistance)
        {
            AlignToPlayer();
            anmtr.SetBool("chasePlayer", false);
        }

        if (playerDistance <= chaseDistance)
        {
            MoveTowardsPlayer();
            anmtr.SetBool("attackPlayer", false);
        }

        if (playerDistance <= attackDistance)
        {
            anmtr.SetBool("chasePlayer", false);
            anmtr.SetBool("attackPlayer", true);
        }

        if (playerDistance > lookDistance && playerDistance > chaseDistance)
        {
            anmtr.SetBool("chasePlayer", false);
        }
    }

    private void AlignToPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0f, rotation.eulerAngles.y, 0f));
    }

    private void MoveTowardsPlayer()
    {
        rb.AddForce(new Vector3(player.position.x, 0f, player.position.z) * movementSpeed, ForceMode.Impulse);
        anmtr.SetBool("chasePlayer", true);
    }

    private void CheckHealth()
    {
        if (creatureHealth <= 0)
        {
            anmtr.SetBool("isDead", true);
            anmtr.SetBool("chasePlayer", false);
            anmtr.SetBool("attackPlayer", false);
        }
    }

    private void RemoveCreature()
    {
        Destroy(gameObject);
    }
}