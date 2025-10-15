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
    private GameObject pickupPiece;
    public bool rightPiece = false;
    private bool addToDict = true;

    private void OnEnable()
    {
        if (addToDict)
        {
            if(!AchievementManager.instance.achievementDictionary.TryAdd(achievementName, this))
            {
                Debug.LogWarning("ERROR: Attempted to add an achievement with a name already in the achievement dictionary. Check and ensure all achivements in the scene have a unique name enumerator.");
            }
            addToDict = false;
        }
    }

    private void UpdateDisplay()
    {
        if (status == AchievementStatus.Hidden)
        {
            achievementPiece.color = Color.black;
            nameText.text = "???";
            descriptionText.text = "???";
            pickupPiece.SetActive(false);
        }
        else if (status == AchievementStatus.Revealed)
        {
            achievementPiece.color = Color.black;
            nameText.text = achievementName.ToString();
            descriptionText.text = achievementDescription;
            pickupPiece.SetActive(false);
        }
        else if (status == AchievementStatus.Achieved)
        {
            achievementPiece.color = Color.white;
            nameText.text = "";
            descriptionText.text = "";
            pickupPiece.SetActive(true);
        }
        else if (status == AchievementStatus.Placed)
        {
            achievementPiece.color = Color.green;
            nameText.text = "";
            descriptionText.text = "";
            pickupPiece.SetActive(false);
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
        pickupPiece = GetComponentInChildren<PuzzlePickup>().gameObject;
        nameText = childTextMeshes[0];
        descriptionText = childTextMeshes[1];
        achievementPiece = GetComponent<SpriteRenderer>();
        UpdateDisplay();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PuzzlePickup>() != null && other.gameObject.GetComponentInParent<Achievement>() == this)
        {
            rightPiece = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PuzzlePickup>() != null && other.gameObject.GetComponentInParent<Achievement>() == this)
        {
            rightPiece = false;
        }
    }
}
