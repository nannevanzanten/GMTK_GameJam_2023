using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class LumberjackBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> Trees;

    private enum LumberState { attacking, searching, walking }

    private LumberState _lumberState;

    private GameObject _closestTree;

    private readonly float _speed = 3f;

    private GameObject closestTree;

    private void Start()
    {
        _lumberState = LumberState.searching;
        Trees.AddRange(GameObject.FindGameObjectsWithTag("tree"));
    }

    private void Update()
    {
        switch (_lumberState)
        {
            case LumberState.searching:
                //Search for the closest tree
                closestTree = GetClosestTree();
                _lumberState = LumberState.walking;
                break;

            case LumberState.walking:
                WalkToClosestTree();

                if (GetDistanceToTree(closestTree) < 0.001f)
                {
                    _lumberState = LumberState.attacking;
                }
                break;

            case LumberState.attacking:
                Trees.Remove(closestTree);
                Destroy(closestTree);
                _lumberState = LumberState.searching;
                break;
        }
    }

    private GameObject GetClosestTree()
    {
        float _maxDistance = 9999f;
        foreach (GameObject obj in Trees)
        {
            float dist = Vector2.Distance(gameObject.transform.position, obj.transform.position);
            if (dist < _maxDistance)
            {
                _closestTree = obj;
                _maxDistance = dist;
            }
        }

        return _closestTree;
    }

    private float GetDistanceToTree(GameObject tree)
    {
        Vector2 distanceVector = transform.position - tree.transform.position;
        return distanceVector.magnitude;
    }

    private void WalkToClosestTree()
    {
        transform.position = Vector2.MoveTowards(transform.position, _closestTree.transform.position, _speed * Time.deltaTime);
    }

    private void AttackTree()
    {
        
    }
}
