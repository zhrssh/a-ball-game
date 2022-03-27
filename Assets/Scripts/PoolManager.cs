using UnityEngine;
using DevZhrssh.Utilities;

public class PoolManager : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    [SerializeField] int poolSize;

    private ObjectPooling objectPooling;

    private void Start()
    {
        objectPooling = GetComponent<ObjectPooling>();
        foreach(GameObject pref in prefabs)
        {
            objectPooling.CreatePool(pref, poolSize);
        }
    }
}
