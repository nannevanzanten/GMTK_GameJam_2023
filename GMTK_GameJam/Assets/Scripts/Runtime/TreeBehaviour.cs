using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    public bool canShoot = true;

    [SerializeField] private float timeBetweenShooting;
    private float shootTime;

    private void Update()
    {
        if (!canShoot)
        {
            resetTimer();
        }
        else if (canShoot)
        {
            shootTime = Time.time;
        }
    }

    private void resetTimer()
    {
        Debug.Log(Time.time - shootTime);
        if (Time.time - shootTime > timeBetweenShooting)
        {
            canShoot = true;
        }
    }
}
