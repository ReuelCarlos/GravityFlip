using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour   // ✅ must match file name
{
    [Header("UI Panels")]
    public GameObject pauseMenuPanel;  // Drag in your PauseMenu Panel

    private bool isPaused = false;

    void Update()
    {
        // Press Escape to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // ====== BUTTON FUNCTIONS ======
    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;   // Resume game time
        isPaused = false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Ensure time is normal before restart
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f; // Reset time before switching
        SceneManager.LoadScene("SplashScreenScene"); // Change to your main menu scene name
    }

    private void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;   // Freeze game time
        isPaused = true;
    }
}
