using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    private float sliderOffset = 5f;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float maxFlinch = 0f;
    [SerializeField]
    private float slamAttackDelay = 0f;
    [SerializeField]
    private Material[] injuryState;

    private Canvas enemyCanvasClone;
    private Slider healthBarClone;
    private SkinnedMeshRenderer sknMeshRndr;
    private Animator anim;

    private float currentFlinch;
    private bool waitActive;

    void Start()
    {
        sknMeshRndr = GetComponent<SkinnedMeshRenderer>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
        currentFlinch = 0;
        UpdateHealthBar();
        InjuryStatus();
    }

    private void Update()
    {
        //healthBar.transform.position = new Vector3 (transform.position.x, transform.position.y + sliderOffset, transform.position.z);
        InjuryStatus();
        if (waitActive == false)
        {
            anim.SetBool("canSlam", false);
        }
    }

    void LateUpdate()
    {
        UpdateHealthBar();
    }

    public void DamageEnemy(float amount)
    {
        currentHealth -= amount;
        currentFlinch++;

        if (currentHealth <= 0)
        {
            anim.SetBool("isDead", true);
            anim.SetBool("chasePlayer", false);
            anim.SetBool("breatheFire", false);
            anim.SetBool("attackPlayer", false);
            Destroy(healthBar);
            
        }
        else if (currentFlinch >= maxFlinch)
        {
            anim.SetBool("canSlam", true);
            StartCoroutine(Wait(slamAttackDelay));            
            currentFlinch = 0;
        }
        else
        {
            anim.SetTrigger("takeDamage");
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
    private IEnumerator Wait(float delay)
    {
        waitActive = true;
        yield return new WaitForSeconds(delay);
        waitActive = false;
    }
}
