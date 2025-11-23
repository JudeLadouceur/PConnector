using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class BarkManager : MonoBehaviour
{
    // Making a Singleton for the script.
    public static BarkManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Function for playing the barks.
    public void PlayBark(EventReference barkEvent)
    {
        EventInstance instance = RuntimeManager.CreateInstance(barkEvent);
        instance.start();
        instance.release();
    }
}