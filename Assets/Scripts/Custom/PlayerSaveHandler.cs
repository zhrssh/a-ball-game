using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZhrssh.Managers.Components;
using DevZhrssh.SaveSystem;
using UnityEngine.Audio;

public class PlayerSaveHandler : MonoBehaviour
{
    public AudioMixer audioMixer;

    private SaveSystem saveSystem;

    private ScoreComponent scoreComponent;
    private PlayerDeathComponent playerDeathComponent;
    private CoinCount coinCountScript;

    public int playCount { get; private set; }

    private void Start()
    {
        // Load Settings
        float volume = PlayerPrefs.GetFloat("volume", 0);
        audioMixer.SetFloat("volume", volume);

        int qualityIndex = PlayerPrefs.GetInt("quality", 3);
        QualitySettings.SetQualityLevel(qualityIndex);

        playerDeathComponent = GameObject.FindObjectOfType<PlayerDeathComponent>();
        saveSystem = GameObject.FindObjectOfType<SaveSystem>();
        scoreComponent = GameObject.FindObjectOfType<ScoreComponent>();
        coinCountScript = GameObject.FindObjectOfType<CoinCount>();

        if (playerDeathComponent != null)
            playerDeathComponent.onPlayerDeathCallback += Save;

        LoadOnStart();
    }
    
    // Load On Start
    private void LoadOnStart()
    {
        SaveData data = saveSystem.Load();
        if (data != null)
        {
            scoreComponent.highScore = data.highScore;
            coinCountScript.SetCointCount(data.currency);
            playCount = data.playCount;
        }
    }

    // Save on death
    public void Save()
    {
        if (saveSystem != null)
        {

            int currentPlayCount = playCount + 1;
            int highScore = scoreComponent.highScore;
            int coinCount = coinCountScript.GetCoinCount();

            SaveData data = new SaveData(highScore, coinCount, currentPlayCount);
            saveSystem.Save(data);
        }
    }
}
