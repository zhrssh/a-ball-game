using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Entity))]
public class PowerUp : MonoBehaviour
{
    protected PowerUpData powerUpData;
    protected PowerUpSystem powerUpSystem;

    private void Start()
    {
        powerUpData = GetComponent<Entity>().entityClass as PowerUpData;
        powerUpSystem = GameObject.FindObjectOfType<PowerUpSystem>();
    }

    public void UsePowerUp(GameObject other)
    {
        if (other.CompareTag("Player") == true)
        {
            // Add the powerup to the system
            powerUpSystem.UsePowerUp(powerUpData);
        }
    }
}
