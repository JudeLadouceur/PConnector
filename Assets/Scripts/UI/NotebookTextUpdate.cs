using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotebookTextUpdate : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    public void SetText(string text)
    {
        textMesh.text = text;
    }
}
