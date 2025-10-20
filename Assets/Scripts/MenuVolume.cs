using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuVolume : MonoBehaviour
{
    [Header("UI")]
    public Slider volumeSlider;
    public TextMeshProUGUI volumeLabel;

    [Header("Audio")]
    public AudioSource musicAudio;

    private void Start()
    {
        // Load saved value (default 100)
        float volumeValue = PlayerPrefs.GetFloat("MenuVolume", 100f);

        volumeSlider.value = volumeValue;
        ApplyVolume(volumeValue);

        // Add listener for slider
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    private void UpdateVolume(float value)
    {
        ApplyVolume(value);
        PlayerPrefs.SetFloat("MenuVolume", value);
    }

    private void ApplyVolume(float value)
    {
        if (volumeLabel != null) volumeLabel.text = "Volume: " + Mathf.RoundToInt(value);
        if (musicAudio != null) musicAudio.volume = value / 100f;
    }
}
