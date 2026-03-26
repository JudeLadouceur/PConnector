using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using FMODUnity;

public class Interactables : MonoBehaviour
{
    public bool ShowsInteractPrompt = false;
    
    public GameObject interactPrompt;
    private GameObject IPRef;

    public float cooldown;

    public string interactPromptText;

    public bool canAddNotes = false;

    private bool canInteract = true;

    protected bool inInteraction = false;

    public EventReference interactAudio;

    private void Awake()
    {
        if (!ShowsInteractPrompt) return;
        IPRef = Instantiate(interactPrompt, transform.parent);
        if (!string.IsNullOrEmpty(interactPromptText)) IPRef.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Press E or click to " + interactPromptText;
        else IPRef.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Press E or click to interact";
        IPRef.transform.GetChild(2).GetComponent<InteractPromptButton>().SetInteractable(this);
        IPRef.SetActive(false);
    }

    public void SetInteractable(bool setToActive)
    {
        if (inInteraction) return;
        if(ShowsInteractPrompt)IPRef.SetActive(setToActive);
        canInteract = setToActive;
    }

    public virtual void Interact()
    {
        if (!canInteract) return;

        //Determine if bark audio should be played
        if (!interactAudio.IsNull)
        {
            DialogueVoiceManager.Instance.PlayBark(interactAudio);
        }
        if (cooldown <= 0) return;
        //Debug.Log("disabling interact prompt");
        SetInteractable(false);
        inInteraction = true;
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        //Debug.Log("enabling interact prompt");
        inInteraction = false;
        SetInteractable(true);

        yield return null;
    }
    private void OnDisable()
    {
        if(IPRef!=null)
            IPRef.SetActive(false);
    }
}
