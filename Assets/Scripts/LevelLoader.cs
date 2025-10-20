using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Called by your UI button
    public void LoadLevel(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is empty! Please assign a valid scene.");
        }
    }

    // Optional: Quit button
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit! (Will not show in Editor)");
    }
}
