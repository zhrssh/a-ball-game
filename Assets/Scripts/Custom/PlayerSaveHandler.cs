using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DevZhrssh.Managers;
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
    private AdsHandler adsHandler;

    private GameManager gameManager;

    [SerializeField] private ShopBallBuyAndEquip shop;

    // Stats
    public int playCount { get; private set; }
    public int gameOvers { get; private set; }

    // Ball
    public int currentEquippedBall { get; private set; }
    public bool ball2 { get; private set; }
    public bool ball3 { get; private set; }
    public bool ball4 { get; private set; }
    public bool ball5 { get; private set; }
    public bool unlockedAll { get; private set; }
    public bool showAds { get; private set; }


    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager != null)
            gameManager.onGameEndCallback += AddGameOverCount;

        // Load Settings
        float volume = PlayerPrefs.GetFloat("volume", 0);
        audioMixer.SetFloat("volume", volume);

        int qualityIndex = PlayerPrefs.GetInt("quality", 3);
        QualitySettings.SetQualityLevel(qualityIndex);

        playerDeathComponent = GameObject.FindObjectOfType<PlayerDeathComponent>();
        saveSystem = GameObject.FindObjectOfType<SaveSystem>();
        scoreComponent = GameObject.FindObjectOfType<ScoreComponent>();
        coinCountScript = GameObject.FindObjectOfType<CoinCount>();
        adsHandler = GameObject.FindObjectOfType<AdsHandler>();

        if (playerDeathComponent != null)
            playerDeathComponent.onPlayerDeathCallback += Save;

        Load();
    }
    
    // Load On Start
    public SaveData Load()
    {
        SaveData data = saveSystem.Load();
        if (data != null)
        {
            scoreComponent.highScore = data.highScore;
            coinCountScript.SetCointCount(data.currency);
            playCount = data.playCount;
            gameOvers = data.deaths;

            currentEquippedBall = data.currentEquippedBall;
            ball2 = data.ball2;
            ball3 = data.ball3;
            ball4 = data.ball4;
            ball5 = data.ball5;
            showAds = data.showAds;
        }

        return data;
    }

    // Save on death
    public void Save()
    {
        if (saveSystem != null)
        {

            int currentPlayCount = playCount;
            int currentDeathCount = gameOvers;
            int highScore = scoreComponent.highScore;
            int coinCount = coinCountScript.GetCoinCount();

            int _currentEquippedBall = 0;
            if (shop != null && shop.isActiveAndEnabled)
            {
                _currentEquippedBall = shop.currentEquippedBall;

                List<int> ballsList = shop.boughtItemsList;
                if (ballsList.Contains(1)) ball2 = true; else ball2 = false;
                if (ballsList.Contains(2)) ball3 = true; else ball3 = false;
                if (ballsList.Contains(3)) ball4 = true; else ball4 = false;
                if (ballsList.Contains(4)) ball5 = true; else ball5 = false;
            } 
            else
            {
                _currentEquippedBall = currentEquippedBall;
            }


            SaveData data = new SaveData(
                highScore,
                coinCount,
                currentPlayCount,
                currentDeathCount,
                _currentEquippedBall,
                ball2,
                ball3,
                ball4,
                ball5,
                adsHandler.showAds
                );

            saveSystem.Save(data);
        }
    }

    public void AddPlayCount()
    {
        playCount = playCount + 1;
        Save();
    }

    public void AddGameOverCount()
    {
        gameOvers = gameOvers + 1;
        Save();
    }
}
