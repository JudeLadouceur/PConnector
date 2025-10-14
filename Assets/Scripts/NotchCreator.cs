using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class NotchCreator : MonoBehaviour
{
    public Vector2 notchCount = new Vector2(3, 2);

    private Vector2 _notchCount = new Vector2(3, 2);

    [Range(10, 200)]
    public int textSize = 24;

    [System.Serializable]
    public class NotchInfo
    {
        public string[] notchNames;
        //Put in reference to caller here
    }

    public NotchInfo notches;

    private GameObject notchParent;
    private GameObject notch;
    private GameObject SBbackground;

    
    private void Awake()
    {
        LoadReferences();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (notchCount != _notchCount)
        {
            notchCount = new Vector2(Mathf.Clamp(notchCount.x, 2, 100), Mathf.Clamp(notchCount.y, 2, 100));
            _notchCount = notchCount;
            BuildNotches();
        }


    }

    [ContextMenu("Reload references")]
    private void LoadReferences()
    {
        notchParent = GameObject.FindGameObjectWithTag("NotchParent");
        notch = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Notch.prefab", typeof(GameObject)) as GameObject;
        SBbackground = GameObject.FindGameObjectWithTag("BoardBG");
    }

    private void BuildNotches()
    {
        if (notchParent.transform.childCount != 0) for (int i = notchParent.transform.childCount - 1; i > -1; i--)
        {
                GameObject.DestroyImmediate(notchParent.transform.GetChild(0).gameObject);
        }

        float xLength = SBbackground.transform.localScale.x * (5f / 6f) / (notchCount.x - 1);
        float yLength = SBbackground.transform.localScale.y * (3f / 4f) / (notchCount.y - 1);

        for (int y = 0; y < notchCount.y; y++)
        {
            for (int x = 0; x < notchCount.x; x++)
            {
                GameObject target = Instantiate(notch, new Vector3(-SBbackground.transform.localScale.x * (5f / 12f) + xLength * x, SBbackground.transform.localScale.y * (3f / 8f) - yLength * y, 0), Quaternion.identity, notchParent.transform);
                target.transform.GetChild(0).GetComponent<TextMeshPro>().fontSize = textSize;
                int notchID = (int)(x + y * notchCount.x);
                if (notchID < notches.notchNames.Length) target.transform.GetChild(0).GetComponent<TextMeshPro>().text = notches.notchNames[notchID];
            }
        }
    }

    [ContextMenu("Rebuild Notches")]
    private void RebuildNotches()
    {
        BuildNotches();
    }
}
