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
    public float bobSpeed = 2f;
    public float bobHeight = 10f;
    public bool randomizeOffset = true;

    private Vector2 bgStartPos;
    private bool skipRequested = false;  // 👈 NEW: flag to detect skip

    private void Start()
    {
        splashPanel.SetActive(true);
        titlePanel.SetActive(false);

        titleCanvasGroup = titlePanel.GetComponent<CanvasGroup>();
        if (titleCanvasGroup == null)
            titleCanvasGroup = titlePanel.AddComponent<CanvasGroup>();

        titleCanvasGroup.alpha = 0;

        titleLogo.alpha = 0;
        menuPanel.alpha = 0;

        bgStartPos = background.anchoredPosition;
        StartCoroutine(PlaySplashSequence());
    }

    void Update()
    {
        // 👇 NEW: Player can tap/click or press any key to skip
        if (Input.anyKeyDown || Input.touchCount > 0)
        {
            skipRequested = true;
        }
    }

    IEnumerator PlaySplashSequence()
    {
        whitePanel.alpha = 1;
        devLogo.alpha = 0;

        // 👇 Fade in/out dev logo (skip check each step)
        yield return StartCoroutine(SkipOrRun(FadeCanvasGroup(devLogo, 0, 1, fadeDuration)));
        if (skipRequested) yield break;
        yield return new WaitForSeconds(logoDuration);
        if (skipRequested) yield break;
        yield return StartCoroutine(SkipOrRun(FadeCanvasGroup(devLogo, 1, 0, fadeDuration)));
        if (skipRequested) yield break;

        yield return StartCoroutine(SkipOrRun(FadeCanvasGroup(whitePanel, 1, 0, fadeDuration)));

        splashPanel.SetActive(false);
        titlePanel.SetActive(true);
        yield return StartCoroutine(SkipOrRun(FadeCanvasGroup(titleCanvasGroup, 0, 1, fadeDuration)));

        spriteContainer.SetActive(true);
        foreach (Transform child in spriteContainer.transform)
        {
            StartCoroutine(PopAnimation(child));
            StartCoroutine(BobAnimation(child));
        }

        // 👇 Background scroll can be skipped
        float traveled = 0f;
        background.anchoredPosition = bgStartPos;
        while (traveled < scrollDistance && !skipRequested)
        {
            float move = scrollSpeed * Time.deltaTime;
            background.anchoredPosition += new Vector2(0, -move);
            traveled += move;
            yield return null;
        }

        // 👇 Immediately jump to final state if skipped
        if (skipRequested)
        {
            SkipToEndState();
            yield break;
        }

        yield return StartCoroutine(SlideAndFadeIn(titleLogo, new Vector2(0, 200), Vector2.zero, fadeDuration));
        yield return StartCoroutine(FadeCanvasGroup(menuPanel, 0, 1, fadeDuration));
    }

    IEnumerator SkipOrRun(IEnumerator routine)
    {
        while (routine.MoveNext())
        {
            if (skipRequested)
            {
                SkipToEndState();
                yield break;
            }
            yield return routine.Current;
        }
    }

    private void SkipToEndState()
    {
        StopAllCoroutines();

        splashPanel.SetActive(false);
        titlePanel.SetActive(true);
        titleCanvasGroup.alpha = 1;
        whitePanel.alpha = 0;
        devLogo.alpha = 0;
        titleLogo.alpha = 1;
        menuPanel.alpha = 1;
        background.anchoredPosition = bgStartPos - new Vector2(0, scrollDistance);
        spriteContainer.SetActive(true);

        // start bobbing animation still
        foreach (Transform child in spriteContainer.transform)
            StartCoroutine(BobAnimation(child));

        Debug.Log("Splash skipped — jumped to main menu instantly");
    }

    // (The rest of your coroutines stay the same)
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
