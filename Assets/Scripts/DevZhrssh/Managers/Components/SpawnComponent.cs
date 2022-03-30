using UnityEngine;
using System.Collections;
using DevZhrssh.Managers;

namespace DevZhrssh.Managers.Components
{
    // Must be in tandem with pool manager and pool component
    public class SpawnComponent : MonoBehaviour
    {
        private PoolManager poolManager;
        private ObjectPoolingComponent poolComponent;

        // Spawning Properties
        [Header("Spawn Properties")]
        [SerializeField] private float minSpawnDelay;
        [SerializeField] private float maxSpawnDelay;
        [SerializeField] private Vector3 minPosition;
        [SerializeField] private Vector3 maxPosition;

        public bool canSpawn = true;

        private GameObject[] prefabs;

        // coroutine handler
        private bool isCoroutineRunning;

        private void Start()
        {
            poolManager = GetComponent<PoolManager>();
            poolComponent = GetComponent<ObjectPoolingComponent>();

            prefabs = poolManager.GetPrefabs();
        }

        private void Update()
        {
            if (canSpawn)
            {
                // Spawns enemy at random range
                if (!isCoroutineRunning)
                {
                    StartCoroutine(SpawnEnemy(
                        prefabs[Random.Range(0, prefabs.Length)],
                        new Vector3(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y), Random.Range(minPosition.z, maxPosition.z)),
                        Quaternion.identity
                        ));
                }
            }
        }

        private IEnumerator SpawnEnemy(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            isCoroutineRunning = true;
            poolComponent.ReuseObject(prefab, position, rotation);
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            isCoroutineRunning = false;
        }
    }
}
