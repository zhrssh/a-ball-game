using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TextMeshProUGUI controlToggle;

    public PlayerController playerController;
    private bool isControlInverted;

    public Slider volumeSlider;
    public TMP_Dropdown graphicsDropdown;

    float volume;
    int qualityIndex;

    private void Start()
    {
        volume = PlayerPrefs.GetFloat("volume", 0);
        audioMixer.SetFloat("volume", volume);
        volumeSlider.value = volume; // UI

        qualityIndex = PlayerPrefs.GetInt("quality", 3);
        QualitySettings.SetQualityLevel(qualityIndex);
        graphicsDropdown.value = qualityIndex; // UI

        isControlInverted = (PlayerPrefs.GetInt("controls", 0) == 0) ? false : true;
        if (!isControlInverted)
        {
            playerController.isControlInverted = false;
            controlToggle.text = "ENABLE";
        }
        else
        {
            playerController.isControlInverted = true;
            controlToggle.text = "DISABLE";
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);

        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        PlayerPrefs.SetInt("quality", qualityIndex);
        PlayerPrefs.Save();
    }

    public void SetControls()
    {
        if (!isControlInverted)
        {
            // Invert controls
            playerController.isControlInverted = true;
            controlToggle.text = "DISABLE";

            PlayerPrefs.SetInt("controls", 1);
            PlayerPrefs.Save();
        }
        else
        {
            // Don't invert controls
            playerController.isControlInverted = false;
            controlToggle.text = "ENABLE";

            PlayerPrefs.SetInt("controls", 0);
            PlayerPrefs.Save();
        }

        isControlInverted = !isControlInverted;
    }
}
