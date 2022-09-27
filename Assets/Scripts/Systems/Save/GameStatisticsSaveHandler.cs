using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;
using DevZhrssh.SaveSystem;
using UnityEngine.Audio;

public class GameStatisticsSaveHandler : MonoBehaviour
{
    // Essential systems and manager
    private GameManager gameManager;
    private SaveSystem saveSystem;

    // Handles audio
    public AudioMixer audioMixer;

    // Components to interact with the save system
    private ScoreComponent scoreComponent;

    // Stats
    public int playCount { get; private set; }
    public int unalives { get; private set; }

    private void Awake()
    {
        // Loads Audio Settings
        float volume = PlayerPrefs.GetFloat("volume", 1);
        audioMixer.SetFloat("volume", volume);

        // Loads Graphics Settings
        int qualityIndex = PlayerPrefs.GetInt("quality", 3);
        QualitySettings.SetQualityLevel(qualityIndex);

        // References
        gameManager = GameObject.FindObjectOfType<GameManager>();
        saveSystem = GameObject.FindObjectOfType<SaveSystem>();
        scoreComponent = GameObject.FindObjectOfType<ScoreComponent>();

        Load();
    }

    private void Start()
    {
        // Adds unalive function to delegate when game ends
        if (gameManager != null)
        {
            gameManager.onGameEndCallback += AddUnaliveCount;
            gameManager.onGameEndCallback += Save;
        }
    }
    
    // Load On Start
    public StatisticsData Load()
    {
        if (saveSystem != null)
        {
            StatisticsData data = saveSystem.Load<StatisticsData>("statistics");
            if (data != null)
            {
                scoreComponent.highScore = data.hiscore;
                playCount = data.playCount;
                unalives = data.unalives;
            } 
            else
            {
                data = new StatisticsData();
            }

            return data;
        }
        else
            return null;
    }

    // Save on death
    public void Save()
    {
        if (saveSystem != null)
        {
            StatisticsData data = new StatisticsData(scoreComponent.highScore, playCount, unalives);
            saveSystem.Save(data, "statistics");
        }
    }

    public void AddPlayCount()
    {
        playCount = playCount + 1;
        Save();
    }

    public void AddUnaliveCount()
    {
        unalives = unalives + 1;
        Save();
    }
}

[System.Serializable]
public class StatisticsData
{
    public int hiscore;
    public int playCount;
    public int unalives;

    public StatisticsData()
    {
        hiscore = 0;
        playCount = 0;
        unalives = 0;
    }

    public StatisticsData(int hiscore, int playCount, int unalives)
    {
        this.hiscore = hiscore;
        this.playCount = playCount;
        this.unalives = unalives;
    }
}
