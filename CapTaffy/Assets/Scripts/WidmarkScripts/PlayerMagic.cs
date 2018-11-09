using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMagic : MonoBehaviour
{
    [SerializeField]
    Slider magicBar;
    [SerializeField]
    Transform projectileSpawnPoint;
    [SerializeField]
    GameObject projectilePrefab;

    public float currentMagic;
    const int maxMagic = 100;
    bool coolDownHasStarted;

    void Start ()
    {
        coolDownHasStarted = false;
        currentMagic = maxMagic;
	}
	
	
	void Update ()
    {
        CastMagic();
        UpdateMagicBar();
        MagicBarCoolDown();
        MagicBarHasCooledDown();
    }

    void CastMagic()
    {
       if (Input.GetKey(KeyCode.M) && currentMagic == maxMagic)
        {
            Fire();
            currentMagic = 0f;
        }
    }

    void UpdateMagicBar()
    {
        magicBar.value = currentMagic / maxMagic;
    }

    void MagicBarCoolDown()
    {
        if (currentMagic < maxMagic)
        {
            coolDownHasStarted = true;
            currentMagic += Time.deltaTime * 50;
        }
    }

    void MagicBarHasCooledDown()
    {
        if (coolDownHasStarted = true && currentMagic >= maxMagic)
        {
            coolDownHasStarted = false;
            currentMagic = 100;
        }
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var projectile = (GameObject)Instantiate(
            projectilePrefab,
            projectileSpawnPoint.position,
            projectileSpawnPoint.rotation);

        // Add velocity to the bullet
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * 30;

        // Destroy the bullet after 2 seconds
        Destroy(projectile, 2.0f);
    }
}
