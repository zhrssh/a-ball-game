using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

using DevZhrssh.Managers;

public class UIOptionsBackend : MonoBehaviour
{
    public AudioMixer audioMixer;

    private AudioManager audioManager;
    private AudioSource music;

    // Control type refers to 0 as slingshot, 1 as flick
    private int controlType;
    public Image slingshot;
    public Image flick;

    public Image yellow;
    public Image gray;

    public Slider volumeSlider;
    public Slider musicSlider;

    float volume;
    int qualityIndex;

    private void Start()
    {
        // Handles Audio Reference
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        if (audioManager != null)
            music = audioManager.GetAudio("BGM");

        // Handles volume
        volume = PlayerPrefs.GetFloat("volumeui", 1);
        volumeSlider.value = volume; // UI
        SetVolume(volume);

        volume = PlayerPrefs.GetFloat("musicui", 1);
        musicSlider.value = volume;
        SetMusicVolume(volume);

        // Handles quality level
        qualityIndex = PlayerPrefs.GetInt("quality", 3);
        QualitySettings.SetQualityLevel(qualityIndex);

        // Handles what is seen by the player
        controlType = (PlayerPrefs.GetInt("controls", 0) == 0) ? 0 : 1;
        if (controlType == 0)
        {
            slingshot.color = yellow.color; // Yellow
            flick.color = gray.color; // Gray
        } 
        else
        {
            slingshot.color = gray.color; // Gray
            flick.color = yellow.color; // Yellow
        }
    }

    public void UpdateUI()
    {
        RefreshReferences();

        volume = PlayerPrefs.GetFloat("volumeui", 1);
        volumeSlider.value = volume; // UI

        volume = PlayerPrefs.GetFloat("musicui", 1);
        musicSlider.value = volume;
    }

    public void RefreshReferences()
    {
        // Handles Audio Reference
        if (audioManager != null)
            music = audioManager.GetAudio("BGM");
    }

    public void SetVolume(float volume)
    {
        float amplified = -80f + volume * 80f;

        if (audioMixer != null)
            audioMixer.SetFloat("volume", amplified);

        PlayerPrefs.SetFloat("volume", amplified);
        PlayerPrefs.SetFloat("volumeui", volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        if (music != null)
            music.volume = volume;

        PlayerPrefs.SetFloat("musicui", volume);
        PlayerPrefs.Save();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        PlayerPrefs.SetInt("quality", qualityIndex);
        PlayerPrefs.Save();
    }

    public void SetControls(int controlType)
    {
        if (controlType == 1)
        {
            // Flick Controls
            // Update UI
            slingshot.color = gray.color; // Gray
            flick.color = yellow.color; // Yellow

            PlayerPrefs.SetInt("controls", 1);
            PlayerPrefs.Save();
        }
        else if (controlType == 0)
        {
            // Slingshot Controls
            // Update UI
            slingshot.color = yellow.color; // Yellow
            flick.color = gray.color; // Gray

            PlayerPrefs.SetInt("controls", 0);
            PlayerPrefs.Save();
        }
    }
}
