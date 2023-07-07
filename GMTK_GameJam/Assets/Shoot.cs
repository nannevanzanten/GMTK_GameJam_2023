using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject acornPrefab;
    [SerializeField] float shootingPower;
    [SerializeField] float flyingTime;

    private Vector3 startPosition = new Vector3(0, 0, 0);
    private bool isAiming;

    private void Update()
    {
        // Start aiming
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isAiming = true;
        }
        // Shoot
        else if (Input.GetMouseButtonUp(0))
        {
            ShootAcorn();
            isAiming = false;
        }
 
        if (isAiming)
        {
            Debug.DrawLine(Vector3.zero, GetLandingPosition(startPosition, CalculateShootingDirection(startPosition)));
        }
    }

    private void ShootAcorn()
    {
        Vector3 shootingDirection = CalculateShootingDirection(startPosition);
        Vector3 landingPosition = GetLandingPosition(startPosition, CalculateShootingDirection(startPosition));

        GameObject acorn = Instantiate(acornPrefab, gameObject.transform);
        acorn.GetComponent<Rigidbody2D>().AddForce(shootingDirection * shootingPower);
    }

    private Vector3 CalculateShootingDirection(Vector3 shootPosition)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootingDirection = shootPosition - mousePosition;

        return shootingDirection;
    }

    private Vector3 GetLandingPosition(Vector3 startPosition, Vector3 shootingDirection)
    {
        Vector3 landingPosition = transform.position + (shootingDirection - startPosition) * flyingTime;
        return landingPosition;
    }
}
