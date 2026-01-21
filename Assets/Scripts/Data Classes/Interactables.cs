using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Interactables : MonoBehaviour
{
    public GameObject interactPrompt;
    private GameObject _interactPrompt;

    public string interactPromptText;

    public bool canAddNotes = false;
    
    
    private void Start()
    {
        _interactPrompt = Instantiate(interactPrompt, transform.parent);
        if (!string.IsNullOrEmpty(interactPromptText)) _interactPrompt.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Press E to " + interactPromptText;
        else _interactPrompt.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Press E to interact";
        _interactPrompt.SetActive(false);
    }

    public void SetInteractable(bool setToActive)
    {
        if (setToActive) _interactPrompt.SetActive(true);
        else _interactPrompt.SetActive(false);
    }

    public virtual void Interact()
    {
        
    }
}
