using System.Collections;
using UnityEngine;

public class ItemVanish : MonoBehaviour
{
    public GameObject Object;
    public float Delay = 5f;

    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = Object.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //start coroutine that being timer to destruction
        Debug.Log("hit");
        StartCoroutine(DelayAction());
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
