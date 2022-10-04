using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;
using DevZhrssh.SaveSystem;
using UnityEngine.UI;

public class AdsReward : MonoBehaviour
{
    private AdsManager adsManager;

    // Score Component
    private ScoreComponent scoreComponent;
    private int scoreToKeep;

    // Coin System
    private GameSystemShopCoinCount coinCount;
    private int currencyToKeep;
 
    // Powerup System
    [SerializeField] private List<GameSystemPowerUpData> powerUpList;
    private GameSystemPowerUp gameSystemPowerUp;
    private string powerUpToGive;

    // Save System to save the data for the next round
    private SaveSystem saveSystem;

    // Game Manager to Load the game
    private GameManager gameManager;

    // Remove button when got rewards
    [SerializeField] private GameObject continueButton;

    public bool hasRecentlyRewarded { get; private set; }


    [System.Serializable]
    public class RewardsData
    {
        public int currency;
        public int score;
        public string powerUp;

        public RewardsData(int currency, int score, string powerUp)
        {
            this.currency = currency;
            this.score = score;
            this.powerUp = powerUp;
        }
    }

    private void Awake()
    {
        // References
        adsManager = AdsManager.Instance;
        gameManager = GameObject.FindObjectOfType<GameManager>();
        saveSystem = GameObject.FindObjectOfType<SaveSystem>();

        scoreComponent = GameObject.FindObjectOfType<ScoreComponent>();
        gameSystemPowerUp = GameObject.FindObjectOfType<GameSystemPowerUp>();
        coinCount = GameObject.FindObjectOfType<GameSystemShopCoinCount>();
    }

    private void Start()
    {
        if (adsManager != null)
        {
            adsManager.onAdCompletedCallback += RewardPlayer;

            // Disable continue button if there is no ads initialized
            if (adsManager.isInitialized == false)
                DisableButton();
        }

        // Get Rewards
        GetRewards();
        if (hasRecentlyRewarded == true)
            DisableButton();
    }

    private void DisableButton()
    {
        if (continueButton != null)
            continueButton.SetActive(false);
    }

    private void RewardPlayer()
    {
        // Rewards player with additional life
        // Reloads the game but scores are kept
        if (scoreComponent != null)
            scoreToKeep = scoreComponent.playerScore;

        // Rewards player with random buff
        if (powerUpList.Count > 0)
            powerUpToGive = powerUpList[Random.Range(0, powerUpList.Count)].name;

        // Keeps the player's currency
        if (coinCount != null)
            currencyToKeep = coinCount.GetCoinCount();

        // Save Data then Reload the game
        if (saveSystem != null)
        {
            RewardsData reward = new RewardsData(currencyToKeep ,scoreToKeep, powerUpToGive);
            saveSystem.Save<RewardsData>(reward, "rewards");
        }

        // Reloads the game
        gameManager.LoadScene("MainLevel");
    }

    private void GetRewards()
    {
        // Get Rewards
        RewardsData rewards = null;
        if (saveSystem != null)
        {
            rewards = saveSystem.Load<RewardsData>("rewards");
        }

        if (rewards != null)
        {
            // Has Rewards
            hasRecentlyRewarded = true;

            // Apply Score
            if (scoreComponent != null)
                scoreComponent.playerScore = rewards.score;

            // Apply currency
            if (coinCount != null)
                coinCount.SetCointCount(rewards.currency);

            // Apply Powerup
            if (gameSystemPowerUp != null)
            {
                for (int i = 0; i < powerUpList.Count; i++)
                {
                    if (powerUpList[i].name == rewards.powerUp)
                    {
                        gameSystemPowerUp.UsePowerUp(powerUpList[i]);
                        break;
                    }
                }
            }

            // Delete data
            saveSystem.DeleteData("rewards");
        }
    }
}
