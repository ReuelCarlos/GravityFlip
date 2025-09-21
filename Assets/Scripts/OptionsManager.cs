using UnityEngine;
using UnityEngine.UI;
using TMPro; // for TextMeshPro labels

public class OptionsManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider volumeSlider;       
    public Slider brightnessSlider;   

    [Header("Labels")]
    public TMP_Text volumeLabel;      // shows "Volume: 75"
    public TMP_Text brightnessLabel;  // shows "Brightness: 60"

    [Header("Brightness Overlay")]
    public Image brightnessOverlay;   

    private void Start()
    {
        // --- Force sliders to use whole numbers ---
        volumeSlider.wholeNumbers = true;
        brightnessSlider.wholeNumbers = true;

        // --- Load saved settings ---
        float savedVolume = PlayerPrefs.GetFloat("Volume", 100f);       // store as 0–100
        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 100f);

        // --- Apply to sliders ---
        volumeSlider.value = savedVolume;
        brightnessSlider.value = savedBrightness;

        // --- Apply effects ---
        ApplyVolume(savedVolume / 100f);          // convert to 0–1 for Unity audio
        ApplyBrightness(savedBrightness / 100f);  // convert to 0–1 for overlay

        // --- Update labels ---
        UpdateVolumeLabel(savedVolume);
        UpdateBrightnessLabel(savedBrightness);

        // --- Add listeners ---
        volumeSlider.onValueChanged.AddListener(SetVolume);
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
    }

    public void SetVolume(float value)
    {
        ApplyVolume(value / 100f);  // Unity expects 0–1
        PlayerPrefs.SetFloat("Volume", value);
        UpdateVolumeLabel(value);
    }

    private void ApplyVolume(float normalized)
    {
        AudioListener.volume = normalized; 
    }

    private void UpdateVolumeLabel(float value)
    {
        if (volumeLabel != null)
            volumeLabel.text = "Volume: " + Mathf.RoundToInt(value);
    }

    public void SetBrightness(float value)
    {
        ApplyBrightness(value / 100f);  // convert to 0–1
        PlayerPrefs.SetFloat("Brightness", value);
        UpdateBrightnessLabel(value);
    }

    private void ApplyBrightness(float normalized)
    {
        if (brightnessOverlay != null)
        {
            Color c = brightnessOverlay.color;
            c.a = 1f - normalized; 
            brightnessOverlay.color = c;
        }
    }

    private void UpdateBrightnessLabel(float value)
    {
        if (brightnessLabel != null)
            brightnessLabel.text = "Brightness: " + Mathf.RoundToInt(value);
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
