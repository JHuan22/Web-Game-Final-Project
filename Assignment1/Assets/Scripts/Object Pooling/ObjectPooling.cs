using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab; // The prefab to be pooled
    public int poolSize; // The number of prefabs to be pooled
    private List<GameObject> pool; // The list of pooled prefabs
    public ObjectPool objectPool;

    private void SpawnPrefabs()
{
    Vector3 spawnPosition = objectPool.transform.position;
    Debug.Log("this is the spawn location" + spawnPosition);
    for (int i = 0; i < 4; i++)
    {
        GameObject obj = objectPool.GetObjectFromPool(spawnPosition);
        obj.SetActive(true);
    }
}

    private void Start()
    {
        pool = new List<GameObject>();

        // Instantiate the prefabs and add them to the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }

        SpawnPrefabs();
    }

    // Get a pooled object from the pool
    public GameObject GetObjectFromPool(Vector3 spawnPosition)
{
    // Find the first inactive object in the pool and return it
    for (int i = 0; i < pool.Count; i++)
    {
        if (!pool[i].activeInHierarchy)
        {
            pool[i].transform.position = spawnPosition;
            pool[i].SetActive(true);
            return pool[i];
        }
    }

    // If no inactive object is found, instantiate a new one and add it to the pool
    GameObject obj = Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
    pool.Add(obj);

    return obj;
}
}
