using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject levelSelectPanel;
    public GameObject optionsPopup;
    public GameObject exitPopup;

    public void NewGame()
    {
        // Load first level directly
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial Level");
    }

    public void LevelSelect()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        levelSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void Options()
    {
        optionsPopup.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPopup.SetActive(false);
    }

    public void ExitGamePopup()
    {
        exitPopup.SetActive(true);
    }

    public void ConfirmExit()
    {
        Application.Quit();
        Debug.Log("Quit Game"); // Works only in build
    }

    public void CancelExit()
    {
        exitPopup.SetActive(false);
    }
}
