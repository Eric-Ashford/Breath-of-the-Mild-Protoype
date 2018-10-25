using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEnemy : MonoBehaviour
{
    [SerializeField]
    private float attackDamage = 15f;

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(attackDamage); // player takes damage

            //this.gameObject.SetActive(false);
        }
    }

}

