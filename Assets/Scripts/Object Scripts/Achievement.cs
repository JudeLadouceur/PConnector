using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows.WebCam;

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
    public Sprite baseSprite;
    public Sprite revealedSprite;

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
        descriptionText.fontSize = 4;
    }

    private void UpdateDisplay()
    {
        if (status == AchievementStatus.Hidden)
        {
            achievementPiece.color = Color.grey;
            nameText.text = "???";
            descriptionText.text = "???";
            pickupPiece.SetActive(false);
        }
        else if (status == AchievementStatus.Revealed)
        {
            achievementPiece.color = Color.grey;
            nameText.text = AchievementManager.instance.namesDict[achievementName];
            descriptionText.text = achievementDescription;
            pickupPiece.SetActive(false);
        }
        else if (status == AchievementStatus.Achieved)
        {
            achievementPiece.sprite = baseSprite;
            achievementPiece.color=Color.white;
            nameText.text = "";
            descriptionText.text = "";
            pickupPiece.SetActive(true);
        }
        else if (status == AchievementStatus.Placed)
        {
            achievementPiece.sprite=revealedSprite;
            achievementPiece.color=Color.white;
            nameText.text = "";
            descriptionText.text = "";
            pickupPiece.SetActive(false);
        }
    }

    public void SetStatus(AchievementStatus newStatus)
    {
        status = newStatus;
        UpdateDisplay();
        if (status== AchievementStatus.Achieved)
        {
            if (AchievementManager.instance && !AchievementManager.instance.endDayRevealed)
                AchievementManager.instance.CheckForEndDayAchieveReveal();
        }
        
    }
    public void Reveal()
    {
        if (status == AchievementStatus.Hidden)
        {
            SetStatus(AchievementStatus.Revealed);
        }
    }
    public void Achieve()
    {
        if (status != AchievementStatus.Placed)
        {
            SetStatus(AchievementStatus.Achieved);
            if (AchievementManager.instance && !AchievementManager.instance.endDayRevealed)
            {

            }
                //AchievementManager.instance.CheckForEndDayAchieveReveal();
        }
    }

    public void SlotPiece()
    {
        if(status== AchievementStatus.Achieved)
        {
            SetStatus(AchievementStatus.Placed);
            if (AchievementManager.instance)
            {
                /*if (AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.PuzzleIntern, out Achievement puzzlePiece))
                {
                    puzzlePiece.Achieve();
                }
                else
                {
                    Debug.Log("Failed to find achievement");
                }*/
            }
            else
            {
                Debug.Log("No Acheivement Manager Found");
            }
        }

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
