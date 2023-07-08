using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackBehaviour : MonoBehaviour
{
    private enum LumberState { attacking, searching, walking }

    private LumberState _lumberState;

    private readonly float _speed = 3f;

    private TreeBehaviour _closestTree;

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
                // If the tree has been destroyed search for new one
                if (_closestTree.isDead)
                {
                    _lumberState = LumberState.searching;
                    break;
                }

                WalkToClosestTree();

                if (GetDistanceToTree(_closestTree.gameObject) < 0.001f)
                {
                    _lumberState = LumberState.attacking;
                }
                break;

            case LumberState.attacking:
                AttackTree();
                break;
        }
    }

    private TreeBehaviour GetClosestTree()
    {
        float _maxDistance = 9999f;
        foreach (TreeBehaviour obj in TreeList.Trees)
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
        StartCoroutine(_closestTree.KillTree());
        _lumberState = LumberState.searching;
    }
}
