using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    [Header("UI Elements")]
    public CanvasGroup whitePanel;   // White background panel
    public CanvasGroup devLogo;      // "Developed by Group 2" logo
    public CanvasGroup titleLogo;    // "Gravity Flip" title logo
    public CanvasGroup menuPanel;    // Panel containing menu buttons

    [Header("Background Scroll")]
    public RectTransform background; // Long vertical background image
    public float scrollSpeed = 50f;  // Pixels per second
    public float scrollDistance = 400f; // How far it scrolls up

    [Header("Timings")]
    public float fadeDuration = 1.5f;
    public float logoDuration = 2f;

    private Vector2 bgStartPos;

    private void Start()
    {
        bgStartPos = background.anchoredPosition;
        StartCoroutine(PlaySplashSequence());
    }

    IEnumerator PlaySplashSequence()
    {
        // STEP 1: White background starts fully visible
        whitePanel.alpha = 1;
        devLogo.alpha = 0;
        titleLogo.alpha = 0;
        menuPanel.alpha = 0;

        // STEP 2: Show dev logo
        yield return StartCoroutine(FadeCanvasGroup(devLogo, 0, 1, fadeDuration));
        yield return new WaitForSeconds(logoDuration);
        yield return StartCoroutine(FadeCanvasGroup(devLogo, 1, 0, fadeDuration));

        // STEP 3: Fade out white panel → reveal background
        yield return StartCoroutine(FadeCanvasGroup(whitePanel, 1, 0, fadeDuration));

        // STEP 4: Scroll background upwards
        float traveled = 0f;
        while (traveled < scrollDistance)
        {
            float move = scrollSpeed * Time.deltaTime;
            background.anchoredPosition += new Vector2(0, -move); // move UP in UI
            traveled += move;
            yield return null;
        }

        // STEP 5: Title logo drops in (slide + fade)
        yield return StartCoroutine(SlideAndFadeIn(titleLogo, new Vector2(0, 200), Vector2.zero, fadeDuration));

        // STEP 6: Menu panel fades in
        yield return StartCoroutine(FadeCanvasGroup(menuPanel, 0, 1, fadeDuration));
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

    IEnumerator SlideAndFadeIn(CanvasGroup cg, Vector2 offset, Vector2 target, float duration)
    {
        float t = 0;
        RectTransform rt = cg.GetComponent<RectTransform>();
        Vector2 startPos = target + offset;
        rt.anchoredPosition = startPos;
        cg.alpha = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            rt.anchoredPosition = Vector2.Lerp(startPos, target, t / duration);
            cg.alpha = Mathf.Lerp(0, 1, t / duration);
            yield return null;
        }

        rt.anchoredPosition = target;
        cg.alpha = 1;
    }
}
