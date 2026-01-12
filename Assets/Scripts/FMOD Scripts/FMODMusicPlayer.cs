using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class FMODMusicPlayer : MonoBehaviour
{
    // Create an EventReference for music, so it can be assigned in the Inspector.
    [SerializeField] EventReference musicEvent;

    private EventInstance musicInstance;

    void Start()
    {
        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        musicInstance.start();
    }

    // Stop and release the track when it gets destroyed, mainly due to a scene transition.
    void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
    }
}