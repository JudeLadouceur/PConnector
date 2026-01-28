using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LandmarkChange : MonoBehaviour
{
    public Sprite brokenSprite;
    public Sprite fixedSprite;

    public DialogueVar fixVariable;

    private void Start()
    {
        if (VariableManager.instance.flags[fixVariable] == 0) GetComponent<SpriteRenderer>().sprite = brokenSprite;
        else GetComponent<SpriteRenderer>().sprite = fixedSprite;
    }
}
