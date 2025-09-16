using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.5f;    // time to fade in/out
    public float displayTime = 2f;       // time fully visible
    public string nextScene = "MainMenu";

    void Start()
    {
        StartCoroutine(FadeSequence());
    }

    private System.Collections.IEnumerator FadeSequence()
    {
        // Ensure alpha starts at 0
        canvasGroup.alpha = 0;

        // Fade In
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(timer / fadeDuration);
            yield return null;
        }

        // Stay visible
        yield return new WaitForSeconds(displayTime);

        // Fade Out
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1 - (timer / fadeDuration));
            yield return null;
        }

        // Load next scene
        SceneManager.LoadScene(nextScene);
    }
}