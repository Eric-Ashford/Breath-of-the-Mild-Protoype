using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;

    //CameraShake camShake;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth = 100f;

    void Start()
    {
        //camShake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void LateUpdate()
    {
        UpdateHealthBar();
    }
    

    public void DamagePlayer(float amount)
    {
        currentHealth -= amount;
        

        if (currentHealth <= 0)
        {
            // TODO: lose game
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
