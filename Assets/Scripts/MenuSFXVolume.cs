using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuSFXVolume : MonoBehaviour
{
    [Header("UI")]
    public Slider sfxSlider;
    public TextMeshProUGUI sfxLabel;

    [Header("Audio")]
    public AudioSource[] sfxAudios; // Assign all SFX AudioSources here

    private void Start()
    {
        // Load saved value (default 100)
        float sfxValue = PlayerPrefs.GetFloat("SFXVolume", 100f);

        sfxSlider.value = sfxValue;
        ApplySFXVolume(sfxValue);

        // Add listener
        sfxSlider.onValueChanged.AddListener(UpdateSFXVolume);
    }

    private void UpdateSFXVolume(float value)
    {
        ApplySFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    private void ApplySFXVolume(float value)
    {
        if (sfxLabel != null) 
            sfxLabel.text = "SFX: " + Mathf.RoundToInt(value);

        if (sfxAudios != null)
        {
            foreach (AudioSource sfx in sfxAudios)
            {
                if (sfx != null)
                    sfx.volume = value / 100f;
            }
        }
    }
}