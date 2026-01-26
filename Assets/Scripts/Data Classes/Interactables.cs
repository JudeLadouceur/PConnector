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

    public string interactPromptText;

    public bool canAddNotes = false;

    private void Start()
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
        
    }
}
