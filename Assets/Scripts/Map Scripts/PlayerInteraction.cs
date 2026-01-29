using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public List<Interactables> interactables;
    public Interactables activeInteractable = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindClosestInteractable());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && activeInteractable != null) activeInteractable.Interact();
        foreach(Interactables i in interactables)
        {
            if (!i.gameObject.activeInHierarchy)
            {
                if (i == activeInteractable)
                {
                    activeInteractable = null;
                }
                interactables.Remove(i);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactables target = collision.gameObject.GetComponentInChildren<Interactables>();
        if (target)
        {
            interactables.Add(target);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactables target = collision.gameObject.GetComponentInChildren<Interactables>();
        if (target)
        {
            interactables.Remove(target);
            activeInteractable.SetInteractable(false);
            if (target = activeInteractable) activeInteractable = null;
        }
    }

    public IEnumerator FindClosestInteractable()
    {
        yield return new WaitForSeconds(0.25f);

        Interactables closestInteractable = null;
        float closestDistance = 10;

        if (activeInteractable != null)
        {
            closestInteractable = activeInteractable;
            closestDistance = Mathf.Sqrt(Mathf.Pow((transform.position.x - closestInteractable.transform.position.x), 2) + Mathf.Pow((transform.position.y - closestInteractable.transform.position.y), 2));
        }

        foreach (Interactables interactable in interactables)
        {
            float distance = Mathf.Sqrt(Mathf.Pow((transform.position.x - interactable.transform.position.x), 2) + Mathf.Pow((transform.position.y - interactable.transform.position.y), 2));
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractable = interactable;
                if(activeInteractable != null) activeInteractable.SetInteractable(false);
                activeInteractable = interactable;
                activeInteractable.SetInteractable(true);
            }
        }

        StartCoroutine(FindClosestInteractable());
    }
}
