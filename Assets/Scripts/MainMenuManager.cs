using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;          // Main menu (Play, Options, Exit buttons)
    public GameObject levelSelectOverlay;     // Level select overlay (transparent black bg + level buttons)
    public GameObject optionsOverlay;         // Options overlay
    public GameObject exitConfirmPanel;       // Exit confirmation panel
    public GameObject tutorialConfirmPanel;   // NEW: Tutorial confirmation panel

    void Start()
    {
        // Initial state: show main menu, hide overlays
        ShowMainMenu();
        if (exitConfirmPanel != null) exitConfirmPanel.SetActive(false);
        if (tutorialConfirmPanel != null) tutorialConfirmPanel.SetActive(false);
    }

    // MAIN MENU BUTTONS
    public void Play()
    {
        mainMenuPanel.SetActive(false);
        levelSelectOverlay.SetActive(true);
    }

    public void Options()
    {
        mainMenuPanel.SetActive(false);
        if (optionsOverlay != null) optionsOverlay.SetActive(true);
    }

    public void Exit()
    {
        if (exitConfirmPanel != null)
        {
            mainMenuPanel.SetActive(false);
            exitConfirmPanel.SetActive(true);
        }
        else
        {
            Application.Quit();
            Debug.Log("Quit Game"); // Only shows in editor
        }
    }

    // EXIT CONFIRMATION BUTTONS
    public void ConfirmExitYes()
    {
        Application.Quit();
        Debug.Log("Quit Game"); // Works only in build
    }

    public void ConfirmExitNo()
    {
        if (exitConfirmPanel != null)
        {
            exitConfirmPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }

    // LOAD LEVELS
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // BACK BUTTONS
    public void BackFromLevelSelect()
    {
        levelSelectOverlay.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void BackFromOptions()
    {
        if (optionsOverlay != null) optionsOverlay.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // TUTORIAL CONFIRMATION
    public void OpenTutorialConfirm()
    {
        if (tutorialConfirmPanel != null)
        {
            mainMenuPanel.SetActive(false);
            tutorialConfirmPanel.SetActive(true);
        }
    }

    public void ConfirmTutorialYes()
    {
        SceneManager.LoadScene("Tutorial"); // Replace with your tutorial scene name
    }

    public void ConfirmTutorialNo()
    {
        if (tutorialConfirmPanel != null)
        {
            tutorialConfirmPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }

    // RESET MAIN MENU STATE
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        if (levelSelectOverlay != null) levelSelectOverlay.SetActive(false);
        if (optionsOverlay != null) optionsOverlay.SetActive(false);
        if (exitConfirmPanel != null) exitConfirmPanel.SetActive(false);
        if (tutorialConfirmPanel != null) tutorialConfirmPanel.SetActive(false);
    }
}
