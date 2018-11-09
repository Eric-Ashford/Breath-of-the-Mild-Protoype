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
    [SerializeField]
    private Material[] injuryState;

    private SkinnedMeshRenderer sknMeshRndr;
    private Animator anim;

    void Start()
    {
        sknMeshRndr = GetComponent<SkinnedMeshRenderer>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        UpdateHealthBar();
        InjuryStatus();
    }

    private void Update()
    {
        InjuryStatus();
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

    public void HealEnemy(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;                
    }

    private void InjuryStatus()
    {
        if (currentHealth / maxHealth >= 2f / 3f)
        {
            sknMeshRndr.sharedMaterial = injuryState[0];
        }
        else if (currentHealth / maxHealth >= 1f / 3f)
        {
            sknMeshRndr.sharedMaterial = injuryState[1];
        }
        else
        {
            sknMeshRndr.sharedMaterial = injuryState[2];
        }
    }
}
