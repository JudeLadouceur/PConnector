using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueRoot : MonoBehaviour
{
    public static DialogueRoot instance;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Second Dialogue Root detected, self destructing the new copy");
            Destroy(gameObject);
        }
    }
}
