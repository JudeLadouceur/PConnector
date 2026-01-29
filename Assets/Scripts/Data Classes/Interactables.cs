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
        if (!string.IsNullOrEmpty(interactPromptText)) IPRef.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Press E to " + interactPromptText;
        else IPRef.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Press E to interact";
        IPRef.SetActive(false);
    }

    public void SetInteractable(bool setToActive)
    {
        if (setToActive) IPRef.SetActive(true);
        else IPRef.SetActive(false);
    }

    public virtual void Interact()
    {
        if (!canInteract) return;
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
        IPRef.SetActive(false);
    }
}
