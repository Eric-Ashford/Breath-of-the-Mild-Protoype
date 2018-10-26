using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheel : MonoBehaviour
{
    [SerializeField]
    GameObject theWeaponWheel;
    [SerializeField]
    GameObject weaponSpawnPoint;
    [SerializeField]
    GameObject weaponOne;
    [SerializeField]
    GameObject weaponTwo;
    [SerializeField]
    GameObject weaponThree;

    bool isPressed;

    const string openWheelAxixName = "openWeaponWheel";

	
	void Start ()
    {
        //sets all weapons as false at Start
        weaponOne.SetActive(false);
        weaponTwo.SetActive(false);
        weaponThree.SetActive(false);
        isPressed = false;
        theWeaponWheel.SetActive(false);
	}
	
	
	void Update ()
    {
        OpenWeaponWheel();
        CloseWeaponWheel(); 
	}

    void FixedUpdate()
    {
        TimeSlow();
    }

    void OpenWeaponWheel() //opens the weapon wheel canvass
    {
        if (Input.GetKey(KeyCode.K) && isPressed == false)
        {
            Cursor.visible = true;
            isPressed = true;
            theWeaponWheel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void CloseWeaponWheel() //closes the weapon wheel canvass
    {
        if (Input.GetKey(KeyCode.L) && isPressed == true)
        {
            Cursor.visible = false;
            isPressed = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            theWeaponWheel.SetActive(false);
            
        }
    }

    public void SelectWeaponOne()
    {
        //activates first weapon on wheel, deactivates any other weapons that might be activated.
        weaponTwo.SetActive(false);
        weaponThree.SetActive(false);
        weaponOne.SetActive(true);
        weaponOne.transform.position = weaponSpawnPoint.transform.position;
    }

    public void SelectWeaponTwo()
    {
        //activates second weapon on wheel, deactivates any other weapons that might be activated.
        weaponOne.SetActive(false);
        weaponThree.SetActive(false);
        weaponTwo.SetActive(true);
        weaponTwo.transform.position = weaponSpawnPoint.transform.position;
    }
    
    public void SelectWeaponThree()
    {
        //activates third weapon on wheel, deactivates any other weapons that might be activated.
        weaponOne.SetActive(false);
        weaponTwo.SetActive(false);
        weaponThree.SetActive(true);
        weaponThree.transform.position = weaponSpawnPoint.transform.position;
    }

    void TimeSlow() 
    {
        // slows down world time while the weapon wheel is open
        if (isPressed == true)
        {
            Time.timeScale = 0.2f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
