using UnityEngine;
using DevZhrssh.Managers.Components;

namespace DevZhrssh.Managers
{
    public class PoolManager : MonoBehaviour
    {
        // Requires ObjectPooling utility as component
        [SerializeField] GameObject[] prefabs;
        [SerializeField] int poolSize;

        private ObjectPoolingComponent objectPooling;

        private void Start()
        {
            objectPooling = GetComponent<ObjectPoolingComponent>();
            foreach(GameObject pref in prefabs)
            {
                objectPooling.CreatePool(pref, poolSize);
            }
        }
    }
}
