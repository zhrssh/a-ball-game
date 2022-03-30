using UnityEngine;
using DevZhrssh.Utilities;
using DevZhrssh.Managers;
using DevZhrssh.Managers.Components;

public class PlayerCollision : MonoBehaviour
{
    // Camera Shake
    [Header("Camera Shake")]
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float shakeDuration = 0.15f;
    [SerializeField] private float shakeMagnitude = 0.4f;

    // Player Death
    [Header("Player Death")]
    [SerializeField] private ParticleSystem deathEffect;
    private AudioManager audioManager;
    private PlayerDeathComponent playerDeathComponent;

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        playerDeathComponent = GameObject.FindObjectOfType<PlayerDeathComponent>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Handle enemy function call
        if (collision.GetComponent<Entity>() as IDamageable != null)
        {
            // Play camera shake when hitting enemies
            if (!collision.GetComponent<Coin>())
                StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude)); // Magic numbers

            // Calls the enemy script OnPlayerCollide
            IDamageable damageable = collision.GetComponent<Entity>() as IDamageable;
            damageable.OnPlayerCollide(gameObject); // pass in the player game object
        } 
    }

    public void Death()
    {
        // Handles player death
        Instantiate(deathEffect, transform.position, Quaternion.identity); // Instantiates death particle
        audioManager.Play("Death"); // Plays death audio
        playerDeathComponent.OnPlayerDeath();// Call Player Death Callback
        gameObject.SetActive(false); // Disables the player
    }
}
