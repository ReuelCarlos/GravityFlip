using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    [Header("Panels")]
    public GameObject splashPanel;   // Holds white BG + dev logo
    public GameObject titlePanel;    // Holds background, title, menu, sprites

    [Header("UI Elements")]
    public CanvasGroup whitePanel;   // White background panel
    public CanvasGroup devLogo;      // "Developed by Group 2" logo
    public CanvasGroup titleLogo;    // "Gravity Flip" title logo
    public CanvasGroup menuPanel;    // Panel containing menu buttons

    [Header("Sprites")]
    public GameObject spriteContainer; // Parent for all your bobbing sprites

    [Header("Background Scroll")]
    public RectTransform background; 
    public float scrollSpeed = 50f;  
    public float scrollDistance = 400f; 

    [Header("Timings")]
    public float fadeDuration = 1.5f;
    public float logoDuration = 2f;

    private Vector2 bgStartPos;

    private void Start()
    {
        splashPanel.SetActive(true);
        titlePanel.SetActive(true);

        // hide until it's their turn
        titleLogo.alpha = 0;
        menuPanel.alpha = 0;
        spriteContainer.SetActive(false);   // 🔹 hide sprites at start

        bgStartPos = background.anchoredPosition;
        StartCoroutine(PlaySplashSequence());
    }

    IEnumerator PlaySplashSequence()
    {
        whitePanel.alpha = 1;
        devLogo.alpha = 0;

        yield return StartCoroutine(FadeCanvasGroup(devLogo, 0, 1, fadeDuration));
        yield return new WaitForSeconds(logoDuration);
        yield return StartCoroutine(FadeCanvasGroup(devLogo, 1, 0, fadeDuration));

        yield return StartCoroutine(FadeCanvasGroup(whitePanel, 1, 0, fadeDuration));

        // 🔹 Reveal sprites right after white fades
        spriteContainer.SetActive(true);

        float traveled = 0f;
        while (traveled < scrollDistance)
        {
            float move = scrollSpeed * Time.deltaTime;
            background.anchoredPosition += new Vector2(0, -move); 
            traveled += move;
            yield return null;
        }

        yield return StartCoroutine(SlideAndFadeIn(titleLogo, new Vector2(0, 200), Vector2.zero, fadeDuration));
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
