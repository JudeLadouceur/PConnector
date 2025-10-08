using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class NotchCreator : MonoBehaviour
{
    public Vector2 notchCount = new Vector2(2, 3);

    public List<GameObject> notches;

    private GameObject notchParent;
    private GameObject notch;

    private void Awake()
    {
        notchParent = GameObject.FindGameObjectWithTag("NotchParent");
        notch = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Notch.prefab", typeof(GameObject));
    }

    // Update is called once per frame
    void Update()
    {
        if (notches.Count != notchCount.x * notchCount.y)
        {
            for (int i = notches.Count - 1; i > -1; i--)
            {
                GameObject.DestroyImmediate(notches[i]);
            }
            notches.Clear();

            float xLength = 10 / notchCount.x;
            float yLength = 6 / notchCount.y;

            for (int y = 0; y < notchCount.y; y++)
            {
                for (int x = 0; x < notchCount.x; x++)
                {
                    GameObject target = Instantiate(notch, new Vector3(-5 + xLength * x, -3 + yLength * y, 0), Quaternion.identity, notchParent.transform);
                    notches.Add(target);
                }
            }
        }
    }

}
