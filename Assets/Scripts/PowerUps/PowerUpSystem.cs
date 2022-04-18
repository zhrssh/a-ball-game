using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSystem : MonoBehaviour
{
    [SerializeField] private GameObject debuffBorder;
    [SerializeField] private GameObject rocketPowerup;
    [SerializeField] private float castRadius = 10f;

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

    private IEnumerator SpawnRockets(PowerUp powerUp)
    {
        float time = powerUp.duration;

        // Allows rockets to spawn when the effect is still in duration
        while (time > 0)
        {
            // Searches for nearest entities
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, castRadius);

            // Limits to spawn count every second
            int spawnCount = (int)powerUp.duration;

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
                if (colliders[i].GetComponent<Entity>()?.entityClass.entityType == EntityClass.EntityType.Deadly)
                {
                    // Spawns the rocket
                    GameObject rocket = Instantiate(rocketPowerup, player.transform.position, Quaternion.identity);

                    // Sets target on the first up to how many is in the duration
                    rocket.GetComponent<Projectile>()?.SetTarget(colliders[i].gameObject);

                    // Audio
                    audioManager.Play("Shoot");

                    // Limit Spawns
                    spawnCount--;
                }

                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }

        // After executing we remove it from list
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
        debuffBorder.SetActive(true);
        playerController.isControlEnabled = false;

        yield return new WaitForSeconds(powerUp.duration);

        // Enable Controls
        debuffBorder.SetActive(false);
        playerController.isControlEnabled = true;

        // When time runs out we remove it from list
        powerUps.Remove(powerUp.name);
    }
}
