using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;        // Main menu (Play, Options, Exit buttons)
    public GameObject levelSelectOverlay;   // Level select overlay (transparent black bg + world buttons)
    public GameObject optionsOverlay;       // Options overlay (transparent black bg)

    [Header("World Panels")]
    public GameObject[] worldPanels;        // Assign World1Panel, World2Panel, etc.

    void Start()
    {
        // Initial state: show main menu, hide overlays
        ShowMainMenu();
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
        Application.Quit();
        Debug.Log("Quit Game"); // only shows in editor
    }

    // LEVEL SELECT
    public void OpenWorld(int worldIndex)
    {
        if (worldIndex >= 0 && worldIndex < worldPanels.Length)
        {
            levelSelectOverlay.SetActive(false);
            worldPanels[worldIndex].SetActive(true);
        }
    }

    public void BackFromWorld(int worldIndex)
    {
        if (worldIndex >= 0 && worldIndex < worldPanels.Length)
        {
            worldPanels[worldIndex].SetActive(false);
            levelSelectOverlay.SetActive(true);
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

    // Called by splash screen or reset
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        levelSelectOverlay.SetActive(false);

        if (optionsOverlay != null) optionsOverlay.SetActive(false);

        foreach (GameObject panel in worldPanels)
        {
            if (panel != null) panel.SetActive(false);
        }
    }
}
