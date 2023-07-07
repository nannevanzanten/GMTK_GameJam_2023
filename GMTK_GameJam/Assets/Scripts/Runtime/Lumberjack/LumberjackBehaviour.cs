using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LumberjackBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    private readonly float _speed = 3f;

    private float _distance;

    private void Update()
    {
        _distance = Vector2.Distance(transform.position, _target.transform.position);
        Vector2 direction = _target.transform.position - transform.position;

        direction.Normalize();

        if (_distance < 5)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        }
    }
}
