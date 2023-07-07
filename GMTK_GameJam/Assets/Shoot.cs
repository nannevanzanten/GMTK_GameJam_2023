using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject acornPrefab;
    [SerializeField] float shootingPower;

    private Vector3 startPosition = new Vector3(0, 0, 0);

    private void Update()
    {
        // Start aiming
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        // Shoot
        else if (Input.GetMouseButtonUp(0))
        {
            ShootAcorn();
        }
    }

    private Vector3 CalculateShootingDirection(Vector3 shootPosition)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootingDirection = shootPosition - mousePosition;

        return shootingDirection;
    }

    private void ShootAcorn()
    {
        Vector3 shootingDirection = CalculateShootingDirection(startPosition);

        GameObject acorn = Instantiate(acornPrefab, gameObject.transform);
        acorn.GetComponent<Rigidbody2D>().AddForce(shootingDirection * shootingPower);
    }
}
