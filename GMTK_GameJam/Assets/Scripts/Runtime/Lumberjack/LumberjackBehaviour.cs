using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LumberjackBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> _trees = new List<GameObject>();

    private Transform _nearestTree;

    private readonly float _speed = 3f;

    private float _distance;

    Transform GetClosestTree(Transform[] trees)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector2 currentPos = transform.position;
        foreach (Transform t in trees)
        {
            float dist = Vector2.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    private void CalculateDistance()
    {
        for (int i = 0; i < _trees.Count; i++)
        {
            Vector2.Distance(_trees[i].transform.position, gameObject.transform.position);
        }
    }

    private void Update()
    {
        CalculateDistance();

        //GetClosestTree(_trees);
        //_distance = Vector2.Distance(transform.position, _trees.transform.position);
        //Vector2 direction = _trees.transform.position - transform.position;

        //direction.Normalize();

        if (_distance < 5)
        {
            //transform.position = Vector2.MoveTowards(transform.position, _trees.transform.position, _speed * Time.deltaTime);
        }
    }
}
