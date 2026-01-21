using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Interactables : MonoBehaviour
{
    public GameObject interactPrompt;
    private GameObject _interactPrompt;

    public Characters character;
    public bool canAddNotes = false;
    
    
    private void Start()
    {
        _interactPrompt = Instantiate(interactPrompt, transform.parent);
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
