using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string InputFootsteps;
    FMOD.Studio.EventInstance FootstepsEvent;
    FMOD.Studio.ParameterInstance CaveParameter;
    FMOD.Studio.ParameterInstance SnowParameter;

    bool playerIsMoving;
    public float walkingSpeed;
    private float CaveValue;
    private float SnowValue;

    void Start()
    {
        FootstepsEvent = FMODUnity.RuntimeManager.CreateInstance(InputFootsteps);
        FootstepsEvent.getParameter("Cave", out CaveParameter);
        FootstepsEvent.getParameter("Snow", out SnowParameter);

        InvokeRepeating("CallFootsteps", .5f, walkingSpeed);
    }


    void Update()
    {
        CaveParameter.setValue(CaveValue);
        SnowParameter.setValue(SnowValue);

        if (Input.GetAxis("Vertical") != 0.00f || Input.GetAxis("Horizontal") != 0.00f)// || Input.GetAxis("Vertical") <= 0.01f || Input.GetAxis("Horizontal") <= 0.01f)
        {
            playerIsMoving = true;
        }
        else if (Input.GetAxis("Vertical") == 0 || Input.GetAxis("Horizontal") == 0)
        {
            playerIsMoving = false;
        }
    }


    void CallFootsteps()
    {
        if (playerIsMoving == true)
        {
            FootstepsEvent.start();

        }
        else if (playerIsMoving == false)
        {
            FootstepsEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }


    void OnDisable()
    {
        playerIsMoving = false;
    }

    //private void OnTriggerStay(Collider MaterialCheck)
    //{
    //    //float FadeSpeed = 10f;

    //    if (MaterialCheck.CompareTag("Cave:Material"))
    //    {
    //        CaveValue = 1f;
    //        SnowValue = 0f;
    //        Debug.Log("HEY, You're on the cave material");
    //        //CaveValue = Mathf.Lerp(CaveValue, 0f, Time.deltaTime * FadeSpeed);
    //        //SnowValue = Mathf.Lerp(SnowValue, 1f, Time.deltaTime * FadeSpeed);
    //    }
    //    if (MaterialCheck.CompareTag("Snow:Material"))
    //    {
    //        CaveValue = 0f;
    //        SnowValue = 1f;
    //        Debug.Log("HEY, You're on the snow material");
    //        //CaveValue = Mathf.Lerp(CaveValue, 1f, Time.deltaTime * FadeSpeed);
    //        //SnowValue = Mathf.Lerp(SnowValue, 0f, Time.deltaTime * FadeSpeed);
    //    }

    //}

    private void OnCollisionEnter(Collision MaterialCheck)
    {
        if (MaterialCheck.gameObject.tag == "Ground")
        {
            CaveValue = 1f;
            SnowValue = 0f;
            Debug.Log("HEY, You're on the cave material");
        }
        else if (MaterialCheck.gameObject.tag == "Snow.Material")
        {
            CaveValue = 0f;
            SnowValue = 1f;
            Debug.Log("HEY, You're on the snow material");
        }
    }

}
