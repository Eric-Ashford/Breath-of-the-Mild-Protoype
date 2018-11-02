using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField]
    GameObject damageSplashScreen;
    [SerializeField]
    private float attackDamage = 15f;

    //CameraShake camShake;
    private bool splashScreenHasBeenActivated;

    void Start()
    {
        //camShake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
        damageSplashScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        SplashScreenOn();
        SplashScreenOff();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            test();
            splashScreenHasBeenActivated = true;
            //camShake.CamShake();
            damageSplashScreen.gameObject.SetActive(true); // Damage splash screen appears
            other.gameObject.GetComponent<PlayerHealth>().DamagePlayer(attackDamage); // player takes damage

            //this.gameObject.SetActive(false);
        }
    }

    void SplashScreenOn()
    {
        if (splashScreenHasBeenActivated == true)
        {
            test();

            damageSplashScreen.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
        }
    }

    void SplashScreenOff()
    {

        if (damageSplashScreen.GetComponent<CanvasGroup>().alpha <= 0 && splashScreenHasBeenActivated == true)
        {
            splashScreenHasBeenActivated = false;
        }
    }


    void test()
    {
        if (splashScreenHasBeenActivated == false && damageSplashScreen.activeSelf == true)
        {
            splashScreenHasBeenActivated = true;
            damageSplashScreen.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}
