using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSwitchboard : MonoBehaviour
{
    private List<Collider2D> notches;
    public static TutorialSwitchboard instance;
    public GameObject notebookPrompt;
    public GameObject visualBlock;
    public GameObject connectPrompt;
    public Button notebookbutton;
    private bool noteFresh;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        notches = new List<Collider2D>();
        Notches[] notchesScripts = GameObject.FindGameObjectWithTag("NotchParent").GetComponentsInChildren<Notches>();
        
        foreach(Notches notch in notchesScripts)
        {
            Collider2D collider = notch.gameObject.GetComponent<Collider2D>();
            notches.Add(collider);
            collider.enabled = false;
        }
        
        notebookPrompt.SetActive(false);
        visualBlock.SetActive(false);
        connectPrompt.SetActive(false);
        notebookbutton.interactable= false;
        noteFresh = true;
    }

    public void EndContextCall()
    {
        notebookPrompt.SetActive(true);
        visualBlock.SetActive(true);
        notebookbutton.interactable = true;
    }

    public void NotebookOpen()
    {
        if (noteFresh)
        {
            notebookPrompt.SetActive(false);
            visualBlock.SetActive(false);
            connectPrompt.SetActive(true);
            foreach(Collider2D collider in notches)
            {
                collider.enabled = true;
            }
            noteFresh = false;
        }
        
    }
    public void NoPrompt()
    {
        notebookPrompt.SetActive(false);
        visualBlock.SetActive(false);
        connectPrompt.SetActive(false);
    }
}
