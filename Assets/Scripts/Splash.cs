using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StardewSplash : MonoBehaviour
{
    public RectTransform background;  // Assign your UI Image here
    public CanvasGroup logo;          // Assign logo CanvasGroup
    public float scrollSpeed = 50f;   // pixels per second
    public float scrollDistance = 500f; // how far to move (in pixels)
    public float fadeDuration = 1.5f;
    public float logoDuration = 2f;

    private Vector2 startPos;

    void Start()
    {
        startPos = background.anchoredPosition;
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        float traveled = 0f;

        // Scroll upwards
        while (traveled < scrollDistance)
        {
            float move = scrollSpeed * Time.deltaTime;
            background.anchoredPosition += new Vector2(0, -move); // move UP
            traveled += move;
            yield return null;
        }

        // Show logo
        yield return StartCoroutine(FadeCanvasGroup(logo, 0, 1, fadeDuration));
        yield return new WaitForSeconds(logoDuration);
        yield return StartCoroutine(FadeCanvasGroup(logo, 1, 0, fadeDuration));

        // After splash → Load MainMenu (or enable it in-scene)
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float t = 0;
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