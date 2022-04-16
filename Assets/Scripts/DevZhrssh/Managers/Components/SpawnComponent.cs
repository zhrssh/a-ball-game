using UnityEngine;
using System.Collections;
using DevZhrssh.Managers;

namespace DevZhrssh.Managers.Components
{
    // Must be in tandem with pool manager and pool component
    public class SpawnComponent : MonoBehaviour
    {
        private GameManager gameManager;
        private PoolManager poolManager;
        private ObjectPoolingComponent poolComponent;

        // Spawning Properties
        [Header("Spawn Properties")]
        [SerializeField] private float minSpawnDelay;
        [SerializeField] private float maxSpawnDelay;
        [SerializeField] private Vector3 minPosition;
        [SerializeField] private Vector3 maxPosition;

        [SerializeField] private bool spawnWithinCameraView;
        [SerializeField] private Vector2 padding;

        [SerializeField] private float spawnRadius;

        public bool canSpawn = true;

        private GameObject[] prefabs;

        // coroutine handler
        private bool isCoroutineRunning;

        private void Start()
        {
            gameManager = GameObject.FindObjectOfType<GameManager>();

            if (gameManager != null)
            {
                gameManager.onGameStartCallback += StartSpawning;
                gameManager.onGameUnpauseCallback += StartSpawning;
                gameManager.onGamePauseCallback += StopSpawning;
                gameManager.onGameEndCallback += StopSpawning;
            }

            poolManager = GetComponent<PoolManager>();
            poolComponent = GetComponent<ObjectPoolingComponent>();

            prefabs = poolManager.GetPrefabs();
        }

        private void StartSpawning()
        {
            canSpawn = true;
        }

        private void StopSpawning()
        {
            canSpawn = false;
        }

        private void Update()
        {
            if (canSpawn)
            {
                // Spawns enemy at random range
                if (!isCoroutineRunning)
                {
                    Vector3 position = Vector3.zero;
                    if (spawnWithinCameraView)
                    {
                        position = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0 + padding.x, Screen.width - padding.x), Random.Range(0 + padding.y, Screen.height - padding.y), Camera.main.farClipPlane / 2));
                    }
                    else
                    {
                        position = new Vector3(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y), Random.Range(minPosition.z, maxPosition.z));
                    }

                    // Can't spawn if false, meaning there is object in the way
                    if (CheckSpawnConditions(position) == false)
                        return;

                    StartCoroutine(SpawnObject(
                        prefabs[Random.Range(0, prefabs.Length)],
                        position,
                        Quaternion.identity
                        ));
                }
            }
        }

        private bool CheckSpawnConditions(Vector3 position)
        {
            // Checks if there is an object on the spawn point
            if (Physics2D.OverlapCircle(position, spawnRadius) != null) // checks if there is an object within radius
                return false;
            else
                return true;
        }

        private IEnumerator SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            isCoroutineRunning = true;
            poolComponent.ReuseObject(prefab, position, rotation);
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            isCoroutineRunning = false;
        }
    }
}
