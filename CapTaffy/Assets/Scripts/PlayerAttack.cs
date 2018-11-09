using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [SerializeField]
    float attackDelay = 0f;

    Animator anim;

    AudioSource[] audioSources;
    AudioSource swordSwing;
    AudioSource swordWhoosh;

    bool canAttack;
    bool waitActive;

    private void Start()
    {
        canAttack = true;
        waitActive = false;

        swordSwing = audioSources[2];
        swordWhoosh = audioSources[3];
    }

    private void Update()
    {
        Attack();
    }


    private void Attack()   //should not be in movement script
    {
        if ((Input.GetButtonDown("Melee Attack") || Input.GetAxis("Melee Attack") > 0.0f) && canAttack)
        {
            canAttack = false;
            anim.SetTrigger("Attack");

            swordSwing.volume = Random.Range(0.9f, 1.1f);
            swordSwing.pitch = Random.Range(0.85f, 1.1f);
            swordSwing.Play();

            swordWhoosh.volume = Random.Range(0.9f, 1.1f);
            swordWhoosh.pitch = Random.Range(0.85f, 1.1f);
            swordWhoosh.Play();
            StartCoroutine(Wait(attackDelay));
            canAttack = true;

        }
    }

    private IEnumerator Wait(float delay)
    {
        waitActive = true;
        yield return new WaitForSeconds(delay);
        waitActive = false;
    }
}
