using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Interactables : MonoBehaviour
{
    public GameObject interactPrompt;
    private GameObject IPRef;

    public float cooldown;

    public string interactPromptText;

    public bool canAddNotes = false;

    private bool canInteract = true;

    private void Awake()
    {
        IPRef = Instantiate(interactPrompt, transform.parent);
        if (!string.IsNullOrEmpty(interactPromptText)) IPRef.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Press E or click to " + interactPromptText;
        else IPRef.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Press E or click to interact";
        IPRef.transform.GetChild(2).GetComponent<InteractPromptButton>().SetInteractable(this);
        IPRef.SetActive(false);
    }

    public void SetInteractable(bool setToActive)
    {
        IPRef.SetActive(setToActive);
        canInteract = setToActive;
    }

    public virtual void Interact()
    {
        if (!canInteract) return;
        if (cooldown <= 0) return;
        canInteract = false;
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);

        canInteract = true;

        yield return null;
    }
    private void OnDisable()
    {
        if(IPRef!=null)
            IPRef.SetActive(false);
    }
}
