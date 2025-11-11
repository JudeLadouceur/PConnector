using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class NotchCreator : MonoBehaviour
{
    //Allows devs to choose how many notches rows and columns there are 
    public Vector2 notchCount = new Vector2(3, 2);

    [Range(10, 200)]
    public int textSize = 24;

    [Range(0.1f, 2)]
    public float notchSize;

    public SO_Character[] characters;

    private GameObject notchParent;
    private GameObject notch;
    private GameObject SBbackground;


    private void Awake()
    {
        LoadReferences();
    }

    //Gets neccesary references for certain objects
    [ContextMenu("Reload references")]
    private void LoadReferences()
    {
        notchParent = GameObject.FindGameObjectWithTag("NotchParent");
#if (UNITY_EDITOR)
        notch = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Notch.prefab", typeof(GameObject)) as GameObject;
#endif
        SBbackground = GameObject.FindGameObjectWithTag("BoardBG");
    }

    public void BuildNotches()
    {
        //Get rid of all existing notches
        if (notchParent.transform.childCount != 0) for (int i = notchParent.transform.childCount - 1; i > -1; i--)
            {
                GameObject.DestroyImmediate(notchParent.transform.GetChild(0).gameObject);
            }

        //Calculate the distance between notches
        float xLength = SBbackground.transform.localScale.x * (5f / 6f) / (notchCount.x - 1);
        float yLength = SBbackground.transform.localScale.y * (3f / 4f) / (notchCount.y - 1);

        notchParent.transform.position = SBbackground.transform.position;

        //Create the notches and set their text size and content
        for (int y = 0; y < notchCount.y; y++)
        {
            for (int x = 0; x < notchCount.x; x++)
            {
                GameObject target = Instantiate(notch, new Vector3(-SBbackground.transform.localScale.x * (5f / 12f) + xLength * x + SBbackground.transform.position.x, SBbackground.transform.localScale.y * (3f / 8f) - yLength * y + SBbackground.transform.position.y, 0), Quaternion.identity, notchParent.transform);
                target.transform.GetChild(1).localScale = new Vector3(notchSize, notchSize);
                target.transform.GetChild(0).localPosition = new Vector3(0, -(notchSize - 1f) / 2f - 0.75f);

                target.transform.GetChild(0).GetComponent<TextMeshPro>().fontSize = textSize;
                int notchID = (int)(x + y * notchCount.x);

                //Assign them the name in the notch name list equal to their value, if it exists
                if (notchID < characters.Length)
                {
                    if (characters[notchID] != null)
                    {
                        target.transform.GetChild(0).GetComponent<TextMeshPro>().text = characters[notchID].characterName;
                        target.transform.GetChild(1).GetComponent<Notches>().assignedCharacter = characters[notchID];
                    }
                    else Debug.LogError("There was no character assigned to notch number " + notchID);
                }
            }
        }
    }
}
