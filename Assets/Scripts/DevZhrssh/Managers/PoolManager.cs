using UnityEngine;
using DevZhrssh.Managers.Components;

namespace DevZhrssh.Managers
{
    [RequireComponent(typeof(ObjectPoolingComponent))]
    public class PoolManager : MonoBehaviour
    {
        // Requires ObjectPooling utility as component
        [SerializeField] private PoolObjects[] prefabs;
        [SerializeField] int poolSize;

        private ObjectPoolingComponent objectPooling;

        [System.Serializable]
        public class PoolObjects
        {
            public GameObject gameObject;
            public float chanceOfSpawning;
        }

        private void Start()
        {
            objectPooling = GetComponent<ObjectPoolingComponent>();
            foreach(PoolObjects pref in prefabs)
            {
                objectPooling.CreatePool(pref.gameObject, poolSize);
            }
        }

        public PoolObjects[] GetPrefabs()
        {
            return prefabs;
        }
    }
}
