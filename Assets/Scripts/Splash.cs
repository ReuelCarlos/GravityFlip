using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StardewSplash : MonoBehaviour
{
    [Header("Background Scroll")]
    public float scrollSpeed = 50f;      // speed of scrolling
    public float scrollDistance = 300f;  // how far it moves

    [Header("Logo Fade")]
    public CanvasGroup logo;             // assign your logo (UI Image) here
    public float fadeDuration = 1.5f;
    public float logoStayTime = 2f;

    [Header("Fade to Black")]
    public CanvasGroup fadePanel;        // full-screen black panel
    public float fadeOutDuration = 1.5f;

    [Header("Scene Change")]
    public string nextScene = "MainMenu";

    private RectTransform bg;
    private float traveled = 0f;

    void Start()
    {
        bg = GetComponent<RectTransform>();
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        // --- Scroll background upward ---
        while (traveled < scrollDistance)
        {
            float move = scrollSpeed * Time.deltaTime;
            bg.anchoredPosition += new Vector2(0, -move); // -move = upward in UI space
            traveled += move;
            yield return null;
        }

        // --- Show logo ---
        yield return StartCoroutine(FadeCanvasGroup(logo, 0, 1, fadeDuration));
        yield return new WaitForSeconds(logoStayTime);
        yield return StartCoroutine(FadeCanvasGroup(logo, 1, 0, fadeDuration));

        // --- Fade to black ---
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0, 1, fadeOutDuration));

        // --- Load next scene ---
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float t = 0f;
        cg.alpha = start;
        while (t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, t / duration);
            yield return null;
        }
        cg.alpha = end;
    }
}