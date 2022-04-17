using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Entity))]
public class PowerUp : MonoBehaviour
{
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private float duration;
    protected EntityClass entityClass;
    protected PowerUpSystem powerUpSystem;

    private void Start()
    {
        entityClass = GetComponent<Entity>()?.entityClass;
        powerUpSystem = GameObject.FindObjectOfType<PowerUpSystem>();
    }

    public void UsePowerUp(GameObject other)
    {
        if (other.CompareTag("Player") == true)
        {
            // If the other is tagged as Player we call the function to display particles
            DisplayParticles(other);

            // Add the powerup to the system
            powerUpSystem.UsePowerUp(entityClass.name, duration);
        }
    }

    private void DisplayParticles(GameObject other)
    {
        // Enable and Disable effect
        if (effect != null)
        {
            ParticleSystem obj = Instantiate(effect, other.transform);
            ParticleSystem.MainModule ma = obj.main;
            ma.startColor = entityClass.color;

            Destroy(obj, duration);
        }
    }

}
