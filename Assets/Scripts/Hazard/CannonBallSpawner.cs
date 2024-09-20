using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallSpawner : MonoBehaviour
{
    //Rename this to CannonBallSpawner
    //Throw this object on where ever, but probs the camera
    //Warning Object Prefab, is going to just be an icon in the 3D space, based along the Z and X axis
    //Toss this on the camera
    [Header("Prefabs")]
    [SerializeField] public GameObject warningObjectPrefab;
    [SerializeField] public GameObject cannonBall;

    [Header("Positioning")]
    [SerializeField] float spawnZRange = 4f;
    [SerializeField] float Xoffset;
    [SerializeField] float Zoffset;
    public float minSpawnInterval = 6f;
    public float maxSpawnInterval = 10f;
    public Transform[] spawns;
    private float currentXValue;
    private float currentZValue;

    public 
    void Start()
    {
        //Call this function at 0s, call it again after this random range of time
        InvokeRepeating("spawnwarning", 0f, Random.Range(minSpawnInterval, maxSpawnInterval));
    }


    void spawnwarning()
    {
        //The spawn is where the random range is gonna be based off of.
        //It should be sorta linked to the camera.
        Transform theSpawn = GetRandomSpawnPoint(spawns);
        float randomZOffSet = Random.Range(-spawnZRange, spawnZRange);
        Vector3 warningSpawnPosition = new Vector3(theSpawn.position.x +9.75f , 0f , theSpawn.position.z + randomZOffSet);
        Instantiate(warningObjectPrefab, warningSpawnPosition, Quaternion.Euler(20,0,0));
        Vector3 cannonBallSpawnPosition = new Vector3(theSpawn.position.x - 10, 0f, theSpawn.position.z + randomZOffSet);
        Instantiate(cannonBall, cannonBallSpawnPosition, Quaternion.Euler(20, 0, 0));
    }

    public Transform GetRandomSpawnPoint(Transform[] Spawners)
    {
        int randomIndex = Random.Range(0, Spawners.Length);
        return Spawners[randomIndex];
    }
    public float GetCurrentZValue()
    {
        return currentZValue;
    }

}

