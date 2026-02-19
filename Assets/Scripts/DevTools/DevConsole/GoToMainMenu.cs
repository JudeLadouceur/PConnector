using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainMenu : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.instance.GoToMainMenu();
    }
}
