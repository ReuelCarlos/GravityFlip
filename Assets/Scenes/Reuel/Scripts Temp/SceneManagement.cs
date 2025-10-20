using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public int nextLevel;
    public int thisLevel;

    public void Restart(){
        SceneManager.LoadScene(thisLevel);
        Time.timeScale = 1f;
    }
    
    public void NextLevel(){
        SceneManager.LoadScene(nextLevel);
        Time.timeScale = 1f;
    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
    }

    public void Pause(){
        Time.timeScale = 0f;
    }
}

