using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPauseRelay : MonoBehaviour
{
    public void OnPause()
    {
        Debug.Log("Pausing...");
        if (PauseMenu.instance)
        {
            //if (context.phase == InputActionPhase.Performed)
            //{
                PauseMenu.instance.OnPause();
            //}
        }
    }

}
