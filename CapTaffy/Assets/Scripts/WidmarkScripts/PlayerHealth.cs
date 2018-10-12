using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;

    //CameraShake camShake;

    float currentHealth;
    const int maxHealth = 100;

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
    

    public void DamagePlayer(int amount)
    {
        currentHealth -= amount;
        

        if (currentHealth <= 0)
        {
            // TODO: lose game
        }
    }

    public void HealPlayer(int amount)
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
