using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectSpawner : MonoBehaviour
{
    public GameObject warningObjectPrefab;
    [SerializeField] float spawnYRange = 100f;
    [SerializeField] float Yoffset;
    public float minSpawnInterval = 4f;
    public float maxSpawnInterval = 6f;
    public Transform[] spawns;
    private float currentXValue;
    private float currentYValue;
    void Start()
    {
        InvokeRepeating("spawnwarning", 0f, Random.Range(minSpawnInterval, maxSpawnInterval));
    }


    void spawnwarning()
    {
        Transform theSpawn = GetRandomSpawnPoint(spawns);
        float randomXoffset = Random.Range(-spawnYRange, spawnYRange);
        Vector3 spawnPosition = new Vector3(theSpawn.position.x + randomXoffset, theSpawn.position.y + Yoffset, 0f);
        Instantiate(warningObjectPrefab, spawnPosition, Quaternion.identity);
    }

    public Transform GetRandomSpawnPoint(Transform[] Spawners)
    {
        int randomIndex = Random.Range(0, Spawners.Length);
        return Spawners[randomIndex];
    }
    public float GetCurrentYValue()
    {
        return currentYValue;
    }

}

