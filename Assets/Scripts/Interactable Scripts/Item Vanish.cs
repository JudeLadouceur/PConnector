using System.Collections;
using FMODUnity;
using UnityEngine;

public class ItemVanish : MonoBehaviour
{
    public GameObject Object;
    public float Delay = 5f;

    private SpriteRenderer sprite;

    public EventReference interactableSounds;

    private void Start()
    {
        sprite = Object.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        //Debug.Log("hit");

        StartCoroutine(DelayAction());

        // Play the respective interactable sounds from FMOD.
        RuntimeManager.PlayOneShot(interactableSounds);
    }

    IEnumerator DelayAction()
    {
        float elapsed = 0f;
        Color color = sprite.color;

        while (elapsed < Delay)
        {
            elapsed += Time.deltaTime;
               //Starts fade out gradually
            float alpha = Mathf.Lerp(1f, 0f, elapsed / Delay);
            sprite.color = new Color(color.r, color.g, color.b, alpha);

            yield return null;
        }

        Destroy(Object);
    }
}