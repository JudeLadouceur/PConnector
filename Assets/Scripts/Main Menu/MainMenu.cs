using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject startButton;

    public GameObject mainMenu;
    public GameObject optionsMenu;

    public GameObject creditsPage;

    public GameObject quitButton;
    public GameObject exitPanel;
    public GameObject achieveBackButton;

    public void StartGame()
    {
        SceneManager.LoadScene("GoalIntro");
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

    public void OpenAchievements()
    {
        AchievementManager.instance.ToggleAchievementView();
        achieveBackButton.SetActive(!achieveBackButton.activeInHierarchy);

        mainMenu.SetActive(!mainMenu.activeInHierarchy);
    }

    public void OpenCredits()
    {
        creditsPage.gameObject.SetActive(true);

        mainMenu.SetActive(false);
    }

    public void CloseCredits()
    {
        creditsPage.gameObject.SetActive(false);

        mainMenu.SetActive(true);
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