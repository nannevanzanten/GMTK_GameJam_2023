using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] GameObject acornPrefab;
    [SerializeField] float shootingPower;
    [SerializeField] float flyingTime;

    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;

    [SerializeField] GameObject aimPoint;

    private bool isAiming;
    private GameObject selectedTree;

    private SelectionManager selectionManager;

    private void Awake()
    {
        selectionManager = FindObjectOfType<SelectionManager>();

        selectionManager.OnSelectTree += SelectionManager_OnSelectTree;
    }

    private void Start()
    {
        selectedTree = selectionManager.GetSelectedTree();
    }

    private void Update()
    {
        // Start aiming
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;
        }
        // Shoot
        else if (Input.GetMouseButtonUp(1))
        {
            ShootAcorn();
            isAiming = false;
        }

        if (isAiming)
        {
            Debug.DrawLine((Vector2)selectedTree.transform.position, GetLandingPosition(CalculateShootingDirection()));
        }
    }

    private void SelectionManager_OnSelectTree(object sender, System.EventArgs e)
    {
        selectedTree = selectionManager.GetSelectedTree();
    }

    private void ShootAcorn()
    {
        Vector2 shootingDirection = CalculateShootingDirection();
        Vector2 landingPosition = GetLandingPosition(CalculateShootingDirection());

        GameObject acorn = Instantiate(acornPrefab, selectedTree.transform.position, Quaternion.identity);
        acorn.GetComponent<Acorn>().goToPosition = landingPosition;
        acorn.GetComponent<Rigidbody2D>().AddForce(shootingDirection * shootingPower);
    }

    private Vector2 CalculateShootingDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootingDirection = (Vector2)selectedTree.transform.position - mousePosition;

        return shootingDirection;
    }

    private Vector2 GetLandingPosition(Vector2 shootingDirection)
    {
        Vector2 distance = (shootingDirection - (Vector2)selectedTree.transform.position) * flyingTime;

        if (distance.magnitude < minDistance)
        {
            distance = distance.normalized * minDistance;
        }
        else if (distance.magnitude > maxDistance)
        {
            distance = distance.normalized * maxDistance;
        }

        Vector2 landingPosition = (Vector2)selectedTree.transform.position + distance;
        return landingPosition;
    }
}
