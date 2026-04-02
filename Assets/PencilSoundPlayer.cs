using FMODUnity;
using UnityEngine;

public class PencilSoundPlayer : MonoBehaviour
{
    public EventReference pencilCollisionSounds;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "The Wire")
        {
            RuntimeManager.PlayOneShot(pencilCollisionSounds);
        }
        else
        {
            return;
        }
    }
}