using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    [Header("Panels")]
    public GameObject splashPanel;   // Holds white BG + dev logo
    public GameObject titlePanel;    // Holds background, title, menu, sprites
    private CanvasGroup titleCanvasGroup;

    [Header("UI Elements")]
    public CanvasGroup whitePanel;
    public CanvasGroup devLogo;
    public CanvasGroup titleLogo;
    public CanvasGroup menuPanel;

    [Header("Sprites")]
    public GameObject spriteContainer; // Parent for bobbing sprites

    [Header("Background Scroll")]
    public RectTransform background;
    public float scrollSpeed = 50f;
    public float scrollDistance = 400f;

    [Header("Timings")]
    public float fadeDuration = 1.5f;
    public float logoDuration = 2f;

    [Header("Bobbing Settings")]
    public float bobSpeed = 2f;       // How fast the bobbing cycles
    public float bobHeight = 10f;     // How high the bobbing goes
    public bool randomizeOffset = true; // Whether each sprite bobs uniquely

    private Vector2 bgStartPos;

    private void Start()
    {
        // At start only splash is visible
        splashPanel.SetActive(true);
        titlePanel.SetActive(false);

        // Safety: add CanvasGroup for title panel
        titleCanvasGroup = titlePanel.GetComponent<CanvasGroup>();
        if (titleCanvasGroup == null)
            titleCanvasGroup = titlePanel.AddComponent<CanvasGroup>();

        titleCanvasGroup.alpha = 0;

        // Hide UI
        titleLogo.alpha = 0;
        menuPanel.alpha = 0;

        bgStartPos = background.anchoredPosition;
        StartCoroutine(PlaySplashSequence());
    }

    IEnumerator PlaySplashSequence()
    {
        // White background visible, dev logo hidden
        whitePanel.alpha = 1;
        devLogo.alpha = 0;

        // Fade in/out dev logo
        yield return StartCoroutine(FadeCanvasGroup(devLogo, 0, 1, fadeDuration));
        yield return new WaitForSeconds(logoDuration);
        yield return StartCoroutine(FadeCanvasGroup(devLogo, 1, 0, fadeDuration));

        // Fade out white panel
        yield return StartCoroutine(FadeCanvasGroup(whitePanel, 1, 0, fadeDuration));

        // Switch panels
        splashPanel.SetActive(false);
        titlePanel.SetActive(true);

        // Fade in entire title screen
        yield return StartCoroutine(FadeCanvasGroup(titleCanvasGroup, 0, 1, fadeDuration));

        // Sprites appear immediately with bobbing
        spriteContainer.SetActive(true);
        foreach (Transform child in spriteContainer.transform)
        {
            StartCoroutine(PopAnimation(child));
            StartCoroutine(BobAnimation(child));
        }

        // Scroll background fully before title logo
        float traveled = 0f;
        background.anchoredPosition = bgStartPos;
        while (traveled < scrollDistance)
        {
            float move = scrollSpeed * Time.deltaTime;
            background.anchoredPosition += new Vector2(0, -move);
            traveled += move;
            yield return null;
        }

        // After scroll finishes → title logo drop in
        yield return StartCoroutine(SlideAndFadeIn(titleLogo, new Vector2(0, 200), Vector2.zero, fadeDuration));

        // Then menu panel fades in
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

    IEnumerator PopAnimation(Transform obj)
    {
        Vector3 originalScale = obj.localScale;
        obj.localScale = originalScale * 0.7f;

        float t = 0;
        float duration = 0.3f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float scale = Mathf.SmoothStep(0.7f, 1.1f, t / duration);
            obj.localScale = originalScale * scale;
            yield return null;
        }

        obj.localScale = originalScale;
    }

    IEnumerator BobAnimation(Transform obj)
    {
        Vector3 startPos = obj.localPosition;
        float offset = randomizeOffset ? Random.Range(0f, Mathf.PI * 2) : 0f;

        while (true)
        {
            float y = Mathf.Sin(Time.time * bobSpeed + offset) * bobHeight;
            obj.localPosition = startPos + new Vector3(0, y, 0);
            yield return null;
        }
    }
}
