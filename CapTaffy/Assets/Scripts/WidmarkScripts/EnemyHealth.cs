using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField]
    Slider healthBar;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth = 100f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void LateUpdate()
    {
        UpdateHealthBar();
    }

    public void DamageEnemy(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            anim.SetBool("isDead", true);
        }
    }

    public void HealPlayer(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;
    }
}
