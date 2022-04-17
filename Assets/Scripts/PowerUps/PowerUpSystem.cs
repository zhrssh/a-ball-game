using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSystem : MonoBehaviour
{
    private List<string> powerUps = new List<string>();

    struct PowerUp
    {
        public string name;
        public float duration;
    }

    // Audio
    private AudioManager audioManager;

    // References
    private ComboSystem comboSystem;
    private Player player;
    private PlayerController playerController;
    private TimeManager timeManager;
    private TimeComponent timeComponent;

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();

        player = GameObject.FindObjectOfType<Player>();
        playerController = player.GetComponent<PlayerController>();

        comboSystem = GameObject.FindObjectOfType<ComboSystem>();

        timeManager = GameObject.FindObjectOfType<TimeManager>();
        timeComponent = timeManager.GetComponent<TimeComponent>();
    }

    public void UsePowerUp(string name, float duration)
    {
        if (!powerUps.Contains(name))
        {
            powerUps.Add(name);

            // Create a new struct to pass on to the coroutine
            PowerUp powerUp = new PowerUp();
            powerUp.name = name;
            powerUp.duration = duration;

            StartCoroutine(name, powerUp); // Starts the corresponding effect
        }
        else
        {
            // Restarts the coroutine
            StopCoroutine(name);

            // Create a new struct to pass on to the coroutine
            PowerUp powerUp = new PowerUp();
            powerUp.name = name;
            powerUp.duration = duration;

            StartCoroutine(name, powerUp);
        }
    }

    // Different Power Up functions

    private IEnumerator DoublePoints(PowerUp powerUp)
    {
        comboSystem.comboMultiplier = 2;
        yield return new WaitForSeconds(powerUp.duration);
        comboSystem.comboMultiplier = 1;

        // When time runs out we remove it from list
        powerUps.Remove(powerUp.name);
    }

    private IEnumerator Invulnerability(PowerUp powerUp)
    {
        player.isInvulnerable = true;
        yield return new WaitForSeconds(powerUp.duration);
        player.isInvulnerable = false;

        // When time runs out we remove it from list
        powerUps.Remove(powerUp.name);
    }

    private IEnumerator FreezeTime(PowerUp powerUp)
    {
        timeManager.runTime = false;
        yield return new WaitForSeconds(powerUp.duration);
        timeManager.runTime = true;

        // When time runs out we remove it from list
        powerUps.Remove(powerUp.name);
    }

    private IEnumerator ExtendTime(PowerUp powerUp)
    {
        timeComponent.AddTime(-powerUp.duration); // We subtract because the current time is a countdown
        yield return null;

        // When effect is done we remove it from list
        powerUps.Remove(powerUp.name);
    }

    private IEnumerator DisableControls(PowerUp powerUp)
    {
        // Disable Controls
        playerController.isControlEnabled = false;

        yield return new WaitForSeconds(powerUp.duration);

        // Enable Controls
        playerController.isControlEnabled = true;

        // When time runs out we remove it from list
        powerUps.Remove(powerUp.name);
    }
}
