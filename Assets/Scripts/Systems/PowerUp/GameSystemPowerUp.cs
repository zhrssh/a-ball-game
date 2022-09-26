using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemPowerUp : MonoBehaviour
{
    [Header("System")]
    [SerializeField] private GameObject debuffBorder;
    [SerializeField] private GameObject rocketPowerup;
    [SerializeField] private float castRadius = 10f;

    private List<string> powerUps = new List<string>();

    // Power up display
    private GameSystemPowerUpDisplay powerUpDisplay;

    // Audio
    private AudioManager audioManager;

    // References
    private GameSystemCombo comboSystem;
    private Player player;
    private PlayerController playerController;
    private TimeManager timeManager;
    private TimeComponent timeComponent;
    private GameSystemShopCoinCount coinCount;

    private void Start()
    {
        powerUpDisplay = GetComponent<GameSystemPowerUpDisplay>();

        audioManager = GameObject.FindObjectOfType<AudioManager>();

        player = GameObject.FindObjectOfType<Player>();
        playerController = player.GetComponent<PlayerController>();

        comboSystem = GameObject.FindObjectOfType<GameSystemCombo>();

        timeManager = GameObject.FindObjectOfType<TimeManager>();
        timeComponent = timeManager.GetComponent<TimeComponent>();

        coinCount = GameObject.FindObjectOfType<GameSystemShopCoinCount>();
    }

    public void UsePowerUp(GameSystemPowerUpData powerUpData)
    {
        if (!powerUps.Contains(powerUpData.name))
        {
            powerUps.Add(powerUpData.name);

            StartCoroutine(powerUpData.name, powerUpData); // Starts the corresponding effect
        }
        else
        {
            // Restarts the coroutine
            StopCoroutine(powerUpData.name);
            StartCoroutine(powerUpData.name, powerUpData);
        }
    }

    // Different Power Up functions

    private IEnumerator DoublePoints(GameSystemPowerUpData powerUp)
    {
        if (comboSystem == null)
            comboSystem = GameObject.FindObjectOfType<GameSystemCombo>();
        
        comboSystem.comboMultiplier = 2;

        // Displays powerup
        powerUpDisplay.DisplayPowerup(powerUp.name);

        // If is a default powerup
        if (powerUp.duration == -1)
            yield break;

        yield return new WaitForSeconds(powerUp.duration);

        // Hides powerup
        powerUpDisplay.HidePowerup(powerUp.name);

        if (comboSystem != null)
            comboSystem.comboMultiplier = 1;

        // When time runs out we remove it from list
        powerUps.Remove(powerUp.name);
    }

    private IEnumerator DoubleCoins(GameSystemPowerUpData powerUp)
    {
        if (coinCount == null)
            coinCount = GameObject.FindObjectOfType<GameSystemShopCoinCount>();
        
        coinCount.SetMultiplier(2);

        // If is a default powerup
        if (powerUp.duration == -1)
        {
            yield break;
        }

        yield return new WaitForSeconds(powerUp.duration);

        if (coinCount != null)
            coinCount.ResetMultiplier();

        powerUps.Remove(powerUp.name);
    }

    private IEnumerator Invulnerability(GameSystemPowerUpData powerUp)
    {
        if (player == null)
            player = GameObject.FindObjectOfType<Player>();

        player.isInvulnerable = true;

        // Displays powerup
        if (powerUpDisplay == null)
            powerUpDisplay = GameObject.FindObjectOfType<GameSystemPowerUpDisplay>();

        powerUpDisplay.DisplayPowerup(powerUp.name);

        yield return new WaitForSeconds(powerUp.duration);

        // Hides powerup
        powerUpDisplay.HidePowerup(powerUp.name);

        player.isInvulnerable = false;

        // When time runs out we remove it from list
        powerUps.Remove(powerUp.name);
    }

    private IEnumerator SpawnRockets(GameSystemPowerUpData powerUp)
    {
        float time = (powerUp.duration == -1) ? 999999 : powerUp.duration;

        // Displays powerup
        if (powerUpDisplay == null)
            powerUpDisplay = GameObject.FindObjectOfType<GameSystemPowerUpDisplay>();

        powerUpDisplay.DisplayPowerup(powerUp.name);

        // Allows rockets to spawn when the effect is still in duration
        if (player == null)
            player = GameObject.FindObjectOfType<Player>();

        while (time > 0)
        {
            // Searches for nearest entities
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, castRadius);

            // Limits to spawn count every second
            int spawnCount = (powerUp.duration == -1) ? 999999 : (int)powerUp.duration;

            // Works like a countdown
            time -= 1f;

            // Checks if the collider has entity script and spawns rockets simultaniously based on number of duration
            for (int i = 0; i < colliders.Length; i++)
            {
                // Prevent null reference
                if (colliders[i] == null) break;

                // If there are no more spawns
                if (spawnCount <= 0) break;

                // If the collider is an entity that kills the player, we spawn a rocket
                if (colliders[i].GetComponent<EntityObject>()?.entityClass.entityType == EntityClass.EntityType.Deadly)
                {
                    // Spawns the rocket
                    GameObject rocket = Instantiate(rocketPowerup, player.transform.position, Quaternion.identity);

                    // Sets target on the first up to how many is in the duration
                    rocket.GetComponent<EntityProjectileRocket>()?.SetTarget(colliders[i].gameObject);

                    // Audio
                    audioManager.Play("Shoot");

                    // Limit Spawns
                    spawnCount--;
                }

                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }

        // Hides powerup
        powerUpDisplay.HidePowerup(powerUp.name);

        // After executing we remove it from list
        powerUps.Remove(powerUp.name);
    }

    private IEnumerator ExtendTime(GameSystemPowerUpData powerUp)
    {
        if (timeComponent != null)
            timeComponent.AddTime(-powerUp.duration); // We subtract because the current time is a countdown
        
        yield return null;

        // When effect is done we remove it from list
        powerUps.Remove(powerUp.name);
    }

    private IEnumerator DisableControls(GameSystemPowerUpData powerUp)
    {
        // Disable Controls
        debuffBorder.SetActive(true);
        playerController.isControlEnabled = false;

        // Displays powerup
        powerUpDisplay.DisplayPowerup(powerUp.name);

        yield return new WaitForSeconds(powerUp.duration);

        // Hides powerup
        powerUpDisplay.HidePowerup(powerUp.name);

        // Enable Controls
        debuffBorder.SetActive(false);
        playerController.isControlEnabled = true;

        // When time runs out we remove it from list
        powerUps.Remove(powerUp.name);
    }
}