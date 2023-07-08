using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] SpawnPoints;
    [SerializeField] private GameObject LumberToSpawn;

    void Start()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            Instantiate(LumberToSpawn, SpawnPoints[i].transform.position, Quaternion.identity);
        }
    }
}
