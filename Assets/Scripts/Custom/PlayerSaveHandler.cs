using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZhrssh.Managers.Components;
using DevZhrssh.SaveSystem;

public class PlayerSaveHandler : MonoBehaviour
{
    private SaveSystem saveSystem;

    private ScoreComponent scoreComponent;
    private PlayerDeathComponent playerDeathComponent;
    private CoinCount coinCountScript;

    private void Start()
    {
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
            Debug.Log("Load Successful!");
            scoreComponent.highScore = data.highScore;
            coinCountScript.SetCointCount(data.currency);
        }
    }

    // Save on death
    public void Save()
    {
        if (saveSystem != null)
        {
            int highScore = scoreComponent.highScore;
            int coinCount = coinCountScript.GetCoinCount();

            SaveData data = new SaveData(highScore, coinCount);
            saveSystem.Save(data);
        }
    }
}
