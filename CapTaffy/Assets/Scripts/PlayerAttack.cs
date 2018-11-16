using System.Collections;
using UnityEngine;


public class PlayerAttack : MonoBehaviour {

    [SerializeField]
    float attackDelay = 0f;

    Animator anim;

    AudioSource[] audioSources;
    AudioSource swordSwing;
    AudioSource swordWhoosh;

    private CapsuleCollider SwordCC;

    bool canAttack;
    bool waitActive;

    [SerializeField]
    private float attackDamage = 15f;

    private bool isAttacking;

    private void Start()
    {
        canAttack = true;
        waitActive = false;

        audioSources = GetComponents<AudioSource>();
        anim = GetComponent<Animator>();
       

        swordSwing = audioSources[2];
        swordWhoosh = audioSources[3];
    }

    private void Update()
    {
        Attack();
    }


    public void Attack()   //should not be in movement script
    {
        if ((Input.GetButtonDown("Melee Attack") || Input.GetAxis("Melee Attack") > 0.0f) && canAttack)
        {
            canAttack = false;
            anim.SetTrigger("Attack");
            isAttacking = true;
           

            swordSwing.volume = Random.Range(0.9f, 1.1f);
            swordSwing.pitch = Random.Range(0.85f, 1.1f);
            swordSwing.Play();

            swordWhoosh.volume = Random.Range(0.9f, 1.1f);
            swordWhoosh.pitch = Random.Range(0.85f, 1.1f);
            swordWhoosh.Play();
            StartCoroutine(Wait(attackDelay));
            
        }
       
        
    }

    private IEnumerator Wait(float delay)
    {
        canAttack = false;
        yield return new WaitForSeconds(delay);
        canAttack = true;
    }

    void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Enemy" && isAttacking == true)
        {
            other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(attackDamage); // player takes damage

            //this.gameObject.SetActive(false);
        }
    }

}
