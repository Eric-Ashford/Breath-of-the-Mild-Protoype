using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEnemy : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(15); // player takes damage

            //this.gameObject.SetActive(false);
        }
    }

}

