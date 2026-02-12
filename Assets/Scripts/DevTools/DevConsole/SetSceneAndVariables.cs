using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class SetSceneAndVariables : MonoBehaviour
{
    public DevConsoleDisplay dayNumber;
    public DevConsoleDisplay time;

    public VariableSetDisplay varName;
    public VariableValDisplay varValue;

    public static SetSceneAndVariables instance;

    private void Awake()
    {
        #if (!UNITY_EDITOR)
        Destroy(gameObject);
        #endif

        #if (UNITY_EDITOR)
        DontDestroyOnLoad(gameObject);
        #endif

        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            GameObject debugMenu = transform.GetChild(0).gameObject; 
            if(!debugMenu.activeSelf) debugMenu.SetActive(true);
            else debugMenu.SetActive(false);
        }
    }

    public void GoToScene()
    {
        /*
        if (time.selectedOption == 0
        {
            string targetScene = "Day " + dayNumber.selectedOption + " Transition";
        }
        */

        //if (time.selectedOption == 1)
        //{
            TimeManager.dayNumber = dayNumber.selectedOption;
            TimeManager.callNumber = 0;
        if (SceneLoadManager.Instance != null)
        {
            SceneLoadManager.Instance.LoadSceneWithFade("Switchboard");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Switchboard");
        }

        //}
    }
    
    public void SetVariable()
    {
        VariableManager.instance.flags[(DialogueVar)varName.selectedOption] = varValue.value;
        Debug.Log("Set variable " + (DialogueVar)varName.selectedOption + " to: " + VariableManager.instance.flags[(DialogueVar)varName.selectedOption]);
    }
}
