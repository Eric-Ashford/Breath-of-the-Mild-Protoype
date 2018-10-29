using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEnemy : MonoBehaviour
{
    [SerializeField]
    private float attackDamage = 15f;

    [FMODUnity.EventRef]
    public string enemyDamageSound;

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(attackDamage); // enemy takes damage

            FMODUnity.RuntimeManager.PlayOneShot(enemyDamageSound);
            //this.gameObject.SetActive(false);
        }
    }

}

