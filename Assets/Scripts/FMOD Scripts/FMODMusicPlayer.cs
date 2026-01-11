using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class FMODMusicPlayer : MonoBehaviour
{
    [SerializeField] EventReference musicEvent;

    private EventInstance musicInstance;

    void Start()
    {
        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInstance.start();
    }

    void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }
}
