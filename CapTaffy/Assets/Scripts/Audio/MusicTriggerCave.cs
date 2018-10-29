using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTriggerCave : MonoBehaviour {

    [FMODUnity.EventRef]
    public string musicTriggerCave;

    FMOD.Studio.EventInstance CaveMusicEvent;

    private void Start()
    {
        CaveMusicEvent = FMODUnity.RuntimeManager.CreateInstance(musicTriggerCave);
    }

    public void OnTriggerEnter(Collider other)
    {
        CaveMusicEvent.start();
    }
}
