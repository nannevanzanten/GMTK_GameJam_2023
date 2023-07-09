using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TimeCycle timeCycle;
    private LumberjackSpawnPoint lumberjackSpawnPoint;

    private int lumberJacksToSpawn;

    private void Awake()
    {
        timeCycle = FindObjectOfType<TimeCycle>();
        lumberjackSpawnPoint = FindObjectOfType<LumberjackSpawnPoint>();

        timeCycle.OnStartDay += TimeCycle_OnStartDay;
    }

    private void TimeCycle_OnStartDay(object sender, System.EventArgs e)
    {
        lumberJacksToSpawn = timeCycle.GetDayCount();
        lumberjackSpawnPoint.SpawnLumberJacks(lumberJacksToSpawn);
    }



}
