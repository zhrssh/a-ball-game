using UnityEngine;
using DevZhrssh.Managers.Components;

public class ObjectSpawningCallbackScript : MonoBehaviour
{
    private PlayerDeathComponent playerDeathComponent;
    private SpawnComponent spawnComponent;

    private void Start()
    {
        playerDeathComponent = GameObject.FindObjectOfType<PlayerDeathComponent>();
        spawnComponent = GameObject.FindObjectOfType<SpawnComponent>();

        playerDeathComponent.onPlayerDeathCallback += StopSpawning;
    }

    private void StopSpawning()
    {
        spawnComponent.canSpawn = false;
    }
}
