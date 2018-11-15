using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;

    //CameraShake camShake;
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    Transform respawnPoint;

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

            //temp solution to losing
            StartCoroutine(Die());
        }
    }

    public void HealPlayer(float amount)
    {
        currentHealth += amount;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;
    }

    IEnumerator Die()
    {
        Destroy(this.gameObject.GetComponent<Rigidbody>());

        yield return new WaitForSecondsRealtime(3.0f);

        currentHealth = maxHealth;
        this.gameObject.transform.position = respawnPoint.position;
    }



}
