using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject startButton;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject quitButton;
    public GameObject exitPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Town Overworld");
    }
    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void OpenAchivements()
    {
        SceneManager.LoadScene("Achievement Development");
    }

    public void OpenExitPanel()
    {
        exitPanel.gameObject.SetActive(true);
    }

    public void CloseExitPanel()
    {
        exitPanel.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}