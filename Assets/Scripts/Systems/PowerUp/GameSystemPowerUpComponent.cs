using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EntityObject))]
public class GameSystemPowerUpComponent : MonoBehaviour
{
    protected GameSystemPowerUpData powerUpData;
    protected GameSystemPowerUp powerUpSystem;

    private void Start()
    {
        powerUpData = GetComponent<EntityObject>().entityClass as GameSystemPowerUpData;
        powerUpSystem = GameObject.FindObjectOfType<GameSystemPowerUp>();
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
