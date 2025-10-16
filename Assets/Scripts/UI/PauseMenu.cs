using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //public PlayerInput playerInput;

    public GameObject pauseMenuCanvas;
    public GameObject pauseMenuButtons;
    public GameObject optionsMenu;
    public GameObject exitPanel;

    private bool gameIsPaused;

    public static PauseMenu instance;

    InputAction togglePause;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        togglePause = InputSystem.actions.FindAction("Pause");
    }
    private void Update()
    {
        if (togglePause.WasPressedThisFrame())
        {
            OnPause();
        }
    }

    public void OnPause()
    {

        if (gameIsPaused)
        {
            ResumeGame();

            //Debug.Log("The game is resumed now.");
        }
        else
        {
            PauseGame();

            //Debug.Log("The game is paused now.");
        }
    }

    public void PauseGame()
    {
        gameIsPaused = true;

        Time.timeScale = 0;

        pauseMenuCanvas.SetActive(true);

        //playerInput.SwitchCurrentActionMap("UI");
    }

    public void ResumeGame()
    {
        gameIsPaused = false;

        Time.timeScale = 1;

        pauseMenuCanvas.SetActive(false);

    }

    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
        pauseMenuButtons.SetActive(false);
    }

    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
        pauseMenuButtons.SetActive(true);
    }

    public void OpenExitPanel()
    {
        exitPanel.gameObject.SetActive(true);

        pauseMenuButtons.SetActive(false);
    }

    public void CloseExitPanel()
    {
        exitPanel.gameObject.SetActive(false);

        pauseMenuButtons.SetActive(true);
    }

    public void QuitGame()
    {
        //playerInput.SwitchCurrentActionMap("Player");
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}