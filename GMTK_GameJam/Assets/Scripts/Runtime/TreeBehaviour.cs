using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    public bool canShoot = true;
    public bool isDead = false;

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
        if (Time.time - shootTime > timeBetweenShooting)
        {
            canShoot = true;
        }
    }

    public IEnumerator KillTree()
    {
        isDead = true;
        yield return new WaitForSeconds(0.001f);
        Destroy(gameObject);
    }
}
