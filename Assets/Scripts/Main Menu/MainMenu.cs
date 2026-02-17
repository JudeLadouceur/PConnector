using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject startButton;

    public GameObject mainMenu;
    public GameObject optionsMenu;

    public GameObject controlsPage;
    public GameObject creditsPage;

    public GameObject quitButton;
    public GameObject exitPanel;
    public GameObject achieveBackButton;

    public void StartGame()
    {
        if (!AchievementManager.instance.achievementDictionary.TryGetValue(AchievementNames.TheEnd, out Achievement achieve) && achieve.status != AchievementStatus.Placed)
        {
            VariableManager.instance.flags[DialogueVar.TutorialComplete] = 1;
        }
        SceneManager.instance.GoToNextScene();
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

    public void OpenControls()
    {
        controlsPage.gameObject.SetActive(true);

        mainMenu.SetActive(false);
    }

    public void CloseControls()
    {
        controlsPage.gameObject.SetActive(false);

        mainMenu.SetActive(true);
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

    public void Tutorial()
    {
        SceneManager.instance.GoToNextScene();
    }
}