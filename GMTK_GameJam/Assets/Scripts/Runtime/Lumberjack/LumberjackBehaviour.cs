using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackBehaviour : MonoBehaviour
{
    private enum LumberState { attacking, searching, walking }

    private LumberState _lumberState;

    private readonly float _speed = 3f;

    private GameObject _closestTree;

    private void Start()
    {
        _lumberState = LumberState.searching;
    }

    private void Update()
    {
        switch (_lumberState)
        {
            case LumberState.searching:
                //Search for the closest tree
                _closestTree = GetClosestTree();
                _lumberState = LumberState.walking;
                break;

            case LumberState.walking:
                WalkToClosestTree();

                if (GetDistanceToTree(_closestTree) < 0.001f)
                {
                    _lumberState = LumberState.attacking;
                }
                break;

            case LumberState.attacking:
                AttackTree();
                break;
        }
    }

    private GameObject GetClosestTree()
    {
        float _maxDistance = 9999f;
        foreach (GameObject obj in TreeList.Trees)
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
        TreeList.Trees.Remove(_closestTree);
        Destroy(_closestTree);
        _lumberState = LumberState.searching;
    }
}
