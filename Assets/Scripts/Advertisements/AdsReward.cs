using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;
using DevZhrssh.SaveSystem;

public class AdsReward : MonoBehaviour
{
    private AdsManager adsManager;

    // Score Component
    private ScoreComponent scoreComponent;
    private int scoreToKeep;

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
    private bool hasRecentlyRewarded;


    [System.Serializable]
    public class Rewards
    {
        public int score;
        public string powerUp;

        public Rewards(int score, string powerUp)
        {
            this.score = score;
            this.powerUp = powerUp;
        }
    }

    private void Start()
    {
        adsManager = AdsManager.Instance;
        if (adsManager != null)
        {
            adsManager.onAdCompletedCallback += RewardPlayer;
        }

        scoreComponent = GameObject.FindObjectOfType<ScoreComponent>();
        gameSystemPowerUp = GameObject.FindObjectOfType<GameSystemPowerUp>();
        saveSystem = GameObject.FindObjectOfType<SaveSystem>();
        gameManager = GameObject.FindObjectOfType<GameManager>();

        // Get Rewards
        GetRewards();
        if (hasRecentlyRewarded == true)
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

        // Save Data then Reload the game
        if (saveSystem != null)
        {
            Rewards reward = new Rewards(scoreToKeep, powerUpToGive);
            saveSystem.Save<Rewards>(reward, "rewards.game");
        }

        // Reloads the game
        gameManager.LoadScene("MainLevel");
    }

    private void GetRewards()
    {
        // Get Rewards
        Rewards rewards = null;
        if (saveSystem != null)
        {
            rewards = saveSystem.Load<Rewards>("rewards.game");
        }

        if (rewards != null)
        {
            // Has Rewards
            hasRecentlyRewarded = true;

            // Apply Score
            if (scoreComponent != null)
                scoreComponent.playerScore = rewards.score;

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
            saveSystem.DeleteData("rewards.game");
        }
    }
}
