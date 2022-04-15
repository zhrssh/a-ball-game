using UnityEngine;
using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;

public class Player : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerCollision playerCollision;
    private PlayerController playerController;
    private TimeControl timeControl;
    private TrajectoryLine trajectoryLine;

    // Player Death
    [Header("Player Death")]
    [SerializeField] private ParticleSystem deathEffect;
    private AudioManager audioManager;
    private PlayerDeathComponent playerDeathComponent;
    public bool isPlayerDead { get; private set; }

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        playerDeathComponent = GameObject.FindObjectOfType<PlayerDeathComponent>();

        gameManager = GameObject.FindObjectOfType<GameManager>();
        playerCollision = GetComponent<PlayerCollision>();
        playerController = GetComponent<PlayerController>();
        timeControl = GetComponent<TimeControl>();
        trajectoryLine = GetComponent<TrajectoryLine>();
        
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
        // Handles player death
        isPlayerDead = true;
        Instantiate(deathEffect, transform.position, Quaternion.identity); // Instantiates death particle
        audioManager.Play("Death"); // Plays death audio
        playerDeathComponent.OnPlayerDeath();// Call Player Death Callback
        gameObject.SetActive(false); // Disables the player
    }
}
