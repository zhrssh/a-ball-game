using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DevZhrssh.SaveSystem;

public class PlayerBall : MonoBehaviour
{
    // Get Current Equipped Ball from save
    [SerializeField] private List<UIShopBall> ballsList;
    [SerializeField] private List<GameSystemPowerUpData> powerUpsAvailable;
    private int currentBall;

    // Save data
    private GamePlayerSaveHandler saveHandler;

    // Apply Current Ball to player
    [SerializeField] private SpriteRenderer playerGFX;

    // Apply powerups
    private GameSystemPowerUp gameSystemPowerUp;
    private GameSystemPowerUpData powerUpToApply;

    private void Awake()
    {
        saveHandler = GameObject.FindObjectOfType<GamePlayerSaveHandler>();
    }

    void Start()
    {
        // Gets the data from the save
        if (saveHandler != null)
            currentBall = saveHandler.playerData.currentBallEquipped;


        // Applies Player GFX
        if (playerGFX != null && ballsList.Count > 0)
        {
            for (int i = 0; i < ballsList.Count; i++)
            {
                if (ballsList[i].GetBallID() == currentBall)
                {
                    playerGFX.color = ballsList[i].image.color;
                    break;
                }
            }
        }

        // Applies Power Ups
        gameSystemPowerUp = GameObject.FindObjectOfType<GameSystemPowerUp>();
        if (gameSystemPowerUp != null)
        {
            for (int i = 0; i < ballsList.Count; i++)
            {
                if (ballsList[i].GetBallID() == currentBall)
                {
                    // Apply powerup depending on the enum
                    switch (ballsList[i].GetAbility())
                    {
                        case UIShopBall.Ability.NONE:
                            break;
                        case UIShopBall.Ability.ROCKETS:
                            powerUpToApply = GetPowerUpData("SpawnRockets");
                            powerUpToApply.duration = -1;
                            gameSystemPowerUp.UsePowerUp(powerUpToApply);
                            break;
                        case UIShopBall.Ability.INVULNERABLE:
                            powerUpToApply = GetPowerUpData("Invulnerability");
                            powerUpToApply.duration = 10;
                            gameSystemPowerUp.UsePowerUp(powerUpToApply);
                            break;
                        case UIShopBall.Ability.DOUBLECOINS:
                            powerUpToApply = GetPowerUpData("DoubleCoins");
                            powerUpToApply.duration = -1;
                            gameSystemPowerUp.UsePowerUp(powerUpToApply);
                            break;
                        case UIShopBall.Ability.RANDO:
                            powerUpToApply = powerUpsAvailable[Random.Range(0, powerUpsAvailable.Count)];
                            gameSystemPowerUp.UsePowerUp(powerUpToApply);
                            break;
                    }
                }
            }
        }
    }

    private GameSystemPowerUpData GetPowerUpData(string name)
    {
        for (int i = 0; i < powerUpsAvailable.Count; i++)
        {
            if (powerUpsAvailable[i].name == name)
            {
                return powerUpsAvailable[i];
            }
        }

        return null;
    }
}

