using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] SpawnPoints;
    [SerializeField] private GameObject LumberToSpawn;

    public void SpawnLumberJacks(int lumberJacksToSpawn)
    {
        for (int i = 0; i < lumberJacksToSpawn; i++)
        {
            GameObject randomSpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)];

            Instantiate(LumberToSpawn, randomSpawnPoint.transform.position, Quaternion.identity);
        }
    }
}
