using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    public bool canShoot = true;
    public bool isDead = false;

    [SerializeField] private float timeBetweenShooting;
    private float shootTime;

    public int Health;

    private void Update()
    {
        if (!canShoot)
        {
            ResetTimer();
        }
        else if (canShoot)
        {
            shootTime = Time.time;
        }
    }

    private void ResetTimer()
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
