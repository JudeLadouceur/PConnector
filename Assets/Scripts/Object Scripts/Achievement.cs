using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Achievement : MonoBehaviour
{
    public AchievementNames achievementName;
    public AchievementStatus status;
    public string achievementDescription;
    public TextMeshPro nameText;
    public TextMeshPro descriptionText;
    public SpriteRenderer achievementPiece;

    private void UpdateDisplay()
    {
        if (status == AchievementStatus.Hidden)
        {
            achievementPiece.color = Color.black;
            nameText.text = "???";
            descriptionText.text = "???";
        }
        else if (status == AchievementStatus.Revealed)
        {
            achievementPiece.color = Color.black;
            nameText.text = achievementName.ToString();
            descriptionText.text = achievementDescription;
        }
        else if (status == AchievementStatus.Achieved)
        {
            achievementPiece.color = Color.white;
            nameText.text = "";
            descriptionText.text = "";
        }
    }

    public void SetStatus(AchievementStatus newStatus)
    {
        status = newStatus;
        UpdateDisplay();
    }

    public void Awake()
    {
        TextMeshPro[] childTextMeshes = GetComponentsInChildren<TextMeshPro>();
        nameText = childTextMeshes[0];
        descriptionText = childTextMeshes[1];
        achievementPiece = GetComponent<SpriteRenderer>();
        UpdateDisplay();
    }
}
