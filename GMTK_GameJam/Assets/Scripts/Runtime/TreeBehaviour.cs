using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    public bool canShoot = false;
    public bool isDead = false;

    public AudioClip _throwNut;
    public AudioClip _landNut;
    public AudioSource _nutSource;

    [SerializeField] private float timeBetweenShooting;
    [SerializeField] private GameObject acorn;
    private float shootTime;

    public int Health;

    private void Start()
    {
        _nutSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!canShoot)
        {
            HideAcorn();
            ResetTimer();
        }
        else if (canShoot)
        {
            if (shootTime != 0)
            {
                ShowAcorn();
            }
            
            shootTime = Time.time;
        }

        if (Health <= 0)
        {
            StartCoroutine(KillTree());
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

    private void ShowAcorn()
    {
        acorn.SetActive(true);
        /*
        if (canShoot)
        {
            acorn.SetActive(true);
        }
        else
        {
            acorn.SetActive(false);
        }
        */
    }

    private void HideAcorn()
    {
        acorn.SetActive(false);
    }
}
