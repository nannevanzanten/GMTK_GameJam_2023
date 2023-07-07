using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject acornPrefab;
    [SerializeField] float shootingPower;
    [SerializeField] float flyingTime;

    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;

    [SerializeField] GameObject aimPoint;

    private bool isAiming;

    private void Update()
    {
        // Start aiming
        if (Input.GetMouseButtonDown(0))
        {
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
            Debug.DrawLine((Vector2)transform.position, GetLandingPosition(CalculateShootingDirection()));

        }
    }

    private void ShootAcorn()
    {
        Vector2 shootingDirection = CalculateShootingDirection();
        Vector2 landingPosition = GetLandingPosition(CalculateShootingDirection());

        GameObject acorn = Instantiate(acornPrefab, gameObject.transform);
        acorn.GetComponent<Acorn>().goToPosition = landingPosition;
        acorn.GetComponent<Rigidbody2D>().AddForce(shootingDirection * shootingPower);
    }

    private Vector3 CalculateShootingDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootingDirection = (Vector2)transform.position - mousePosition;

        return shootingDirection;
    }

    private Vector3 GetLandingPosition(Vector2 shootingDirection)
    {
        Vector2 distance = (shootingDirection - (Vector2)transform.position) * flyingTime;
   
        if (distance.magnitude < minDistance)
        {
            distance = distance.normalized * minDistance;
        }
        else if (distance.magnitude > maxDistance)
        {
            distance = distance.normalized * maxDistance;
        }

        Vector3 landingPosition = (Vector2)transform.position + distance;
        return landingPosition;
    }
}
