using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SetSceneAndVariables : MonoBehaviour
{
    public DevConsoleDisplay dayNumber;
    //public DevConsoleDisplay time;

    public VariableSetDisplay varName;
    public TextMeshProUGUI varValue;

    private void Awake()
    {
        #if (!UNITY_EDITOR)
        Destroy(gameObject);
        #endif

        #if (UNITY_EDITOR)
        DontDestroyOnLoad(gameObject);
        #endif
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
            SceneManager.LoadScene("Switchboard");
        //}
    }
    
    public void SetVariable()
    {
        Debug.Log(varValue.text);
        int value; 
        int.TryParse(varValue.text, out value);
        Debug.Log(value);

        value = int.Parse(varValue.text, System.Globalization.NumberStyles.Integer)

        value = Convert.ToInt32(varValue.text);
        Debug.Log(value);

        VariableManager.instance.flags[(DialogueVar)varName.selectedOption] = value;
        Debug.Log("Set variable " + (DialogueVar)varName.selectedOption + " to: " + VariableManager.instance.flags[(DialogueVar)varName.selectedOption]);
    }
}
