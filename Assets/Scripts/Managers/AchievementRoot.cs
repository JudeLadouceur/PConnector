using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementRoot : MonoBehaviour
{
    private static AchievementRoot instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
            Debug.LogWarning("Second Achievement Root detected, self destructing the new copy");
        }
    }
}
