using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class CharacterAndDialogue : MonoBehaviour
{
    public string Name;
    public GameObject Dialogue;
    public GameObject Character;
    private GameObject DialogueIntsatiate;


    private void Start()
    {
       DialogueIntsatiate = Instantiate(Dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DialogueIntsatiate.SetActive(true);

            if(AchievementManager.instance && AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.LittleTalks, out Achievement value) && value.status == AchievementStatus.Revealed)
            {
                value.Achieve();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) DialogueIntsatiate.SetActive(false);
    }


}
