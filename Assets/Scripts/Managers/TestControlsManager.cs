using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class TestControlsManager : MonoBehaviour
{
    private InputAction toggleAchieve;
    private void Start()
    {
        toggleAchieve = InputSystem.actions.FindAction("Toggle Achievements");
    }
    private void Update()
    {
        if (toggleAchieve.WasPressedThisFrame())
        {
            AchievementManager.instance.ToggleAchievementView();
        }
    }

}
