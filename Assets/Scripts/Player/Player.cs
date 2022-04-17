using UnityEngine;
using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;

public class Player : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerCollision playerCollision;
    private PlayerController playerController;
    private TimeControl timeControl;
    private Trajectory trajectoryLine;

    private SpriteRenderer spriteRenderer;

    // Player Death
    [Header("Player Death")]
    [SerializeField] private ParticleSystem deathEffect;
    private AudioManager audioManager;
    private PlayerDeathComponent playerDeathComponent;
    public bool isPlayerDead { get; private set; }
    public bool isInvulnerable;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        audioManager = GameObject.FindObjectOfType<AudioManager>();
        playerDeathComponent = GameObject.FindObjectOfType<PlayerDeathComponent>();

        gameManager = GameObject.FindObjectOfType<GameManager>();
        playerCollision = GetComponent<PlayerCollision>();
        playerController = GetComponent<PlayerController>();
        timeControl = GetComponent<TimeControl>();
        trajectoryLine = GetComponent<Trajectory>();
        
        if (gameManager != null)
        {
            gameManager.onGameStartCallback += EnableScripts;
            gameManager.onGameUnpauseCallback += EnableScripts;
            gameManager.onGameEndCallback += DisableScripts;
            gameManager.onGamePauseCallback += DisableScripts;
        }
    }

    private void EnableScripts()
    {
        playerCollision.enabled = true;
        playerController.enabled = true;
        timeControl.enabled = true;
        trajectoryLine.enabled = true;
    }

    private void DisableScripts()
    {
        playerCollision.enabled = false;
        playerController.enabled = false;
        timeControl.enabled = false;
        trajectoryLine.enabled = false;
    }
    public void Death()
    {
        // Won't die if player is invulnerable
        if (isInvulnerable) return;

        // Handles player death
        isPlayerDead = true;

        // Spawns particles based on color
        ParticleSystem particles = Instantiate(deathEffect, transform.position, Quaternion.identity); // Instantiates death particle
        ParticleSystem.MainModule ma = particles.main;
        ma.startColor = spriteRenderer.color;

        audioManager.Play("Death"); // Plays death audio
        playerDeathComponent.OnPlayerDeath();// Call Player Death Callback
        gameObject.SetActive(false); // Disables the player
    }
}
