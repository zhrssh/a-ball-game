using System.Collections.Generic;
using UnityEngine;

namespace DevZhrssh.Utilities
{
    public class ObjectPooling : MonoBehaviour
    {
        private Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();
        public void CreatePool(GameObject prefab, int poolSize)
        {
            // Used as a key for the dictionary
            int prefabKey = prefab.GetInstanceID();

            if (!poolDictionary.ContainsKey(prefabKey))
            {
                // Used to store the pooled objects
                GameObject poolHolder = new GameObject(prefab.name + " pool");
                poolHolder.transform.parent = transform;

                poolDictionary.Add(prefabKey, new Queue<ObjectInstance>());

                // Instantiates the prefab or the pooled object depending on the pool size
                for (int i = 0; i < poolSize; i++)
                {
                    ObjectInstance obj = new ObjectInstance(Instantiate(prefab) as GameObject);
                    poolDictionary[prefabKey].Enqueue(obj);
                    obj.SetParent(poolHolder.transform);
                }
            }
        }

        public void ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            // Gets the prefab key to be used on the dictionary
            int prefabKey = prefab.GetInstanceID();

            if (poolDictionary.ContainsKey(prefabKey))
            {
                // Dequeue and enqueue allows the obj to be placed on the last index of the queue
                ObjectInstance obj = poolDictionary[prefabKey].Dequeue();
                poolDictionary[prefabKey].Enqueue(obj);
                obj.Reuse(position, rotation);
            }
        }

        public class ObjectInstance
        {
            GameObject gameObject;
            Transform transform;

            bool hasPooledObjectScript;
            PooledObject pooledObjectScript;

            public ObjectInstance(GameObject gameObject)
            {
                this.gameObject = gameObject;
                transform = gameObject.transform;
                gameObject.SetActive(false);

                if (gameObject.GetComponent<PooledObject>() != null)
                {
                    hasPooledObjectScript = true;
                    pooledObjectScript = gameObject.GetComponent<PooledObject>();
                }
            }

            public void Reuse(Vector3 position, Quaternion rotation)
            {
                if (hasPooledObjectScript)
                    pooledObjectScript.OnObjectReuse();

                gameObject.SetActive(true);
                transform.position = position;
                transform.rotation = rotation;
            }

            public void SetParent(Transform parent)
            {
                gameObject.transform.parent = parent;
            }
        }
    }

    // Script that is meant for the pooled object (optional)
    public class PooledObject : MonoBehaviour
    {
        public virtual void OnObjectReuse()
        {
            // meant to be overriden
        }
    }
}
