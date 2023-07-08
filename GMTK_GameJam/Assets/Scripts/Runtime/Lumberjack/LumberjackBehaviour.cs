using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class LumberjackBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> Trees;

    private enum LumberState { Attack, Search, Stunned }

    private LumberState _lumberState;

    private GameObject _closestTree;

    private readonly float _speed = 3f;

    private float _maxDistance = 9999f;

    private void Start()
    {
        _lumberState = LumberState.Search;
        Trees.AddRange(GameObject.FindGameObjectsWithTag("tree"));
    }

    private void CalculateClosestTree()
    {
        foreach (GameObject obj in Trees)
        {
            float dist = Vector2.Distance(gameObject.transform.position, obj.transform.position);
            if (dist < _maxDistance && _lumberState == LumberState.Search)
            {
                _closestTree = obj;
                _maxDistance = dist;

                transform.position = Vector2.MoveTowards(transform.position, obj.transform.position, _speed * Time.deltaTime);
            }
        }
    }

    private void AttackTree()
    {

    }

    private void Update()
    {
        CalculateClosestTree();
        print(_closestTree);
    }
}
