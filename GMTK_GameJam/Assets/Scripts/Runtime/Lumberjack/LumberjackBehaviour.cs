using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    private GameObject bigTree;

    private TreeBehaviour _closestTree;
    private TimeCycle _timeCycle;
    private LumberjackSpawnPoint _spawnPoint;
    private GameObject _closestHouse;
    private GameManager _gameManager;

    private enum LumberState { attacking, searching, walking, sleeping, finishing }

    private LumberState _lumberState;

    private readonly float _speed = 3f;

    private readonly int _damage = 1;
    private float _damageInterval = 1;
    private bool _canAttack;

    private void Start()
    {
        _spawnPoint = FindObjectOfType<LumberjackSpawnPoint>();
        _timeCycle = FindObjectOfType<TimeCycle>();
        _gameManager = FindObjectOfType<GameManager>();

        _lumberState = LumberState.searching;

        _timeCycle.OnStartNight += TimeCycle_OnStartNight;
    }

    private void Update()
    {
        switch (_lumberState)
        {
            case LumberState.searching:
                // If there are no trees go to finish state
                if (TreeList.Trees.Count == 0)
                {
                    _lumberState = LumberState.finishing;
                    break;
                }

                //Search for the closest tree
                _closestTree = GetClosestTree();
                _lumberState = LumberState.walking;
                break;

            case LumberState.walking:
                // If the tree has been destroyed search for new one
                if (TreeList.Trees.Count > 0)
                {
                    FaceObject(_closestTree.gameObject);
                }

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
                if (_canAttack)
                {
                    _canAttack = false;
                    _damageInterval = Time.time;
                    AttackTree();
                }
                else
                {
                    ResetTimer();
                }
                break;

            case LumberState.sleeping:
                _closestHouse = FindClosestHouse();
                FaceObject(_closestHouse);
                LumberSleep();

                if (GetDistanceToHouse(_closestHouse) < 0.001f)
                {
                    Destroy(gameObject);
                }
                break;

            case LumberState.finishing:
                // If there is a new tree return to search state
                if (TreeList.Trees.Count > 0)
                {
                    _lumberState = LumberState.searching;
                    break;
                }

                if (bigTree == null)
                {
                    bigTree = GameObject.FindGameObjectWithTag("BigTree");
                }

                AttackBigTree();
                break;
        }
    }

    private void FaceObject(GameObject obj)
    {
        if (obj.transform.position.x < gameObject.transform.position.x)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void ResetTimer()
    {
        if (Time.time - _damageInterval > 1f)
        {
            _canAttack = true;
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
        _closestTree.Health -= _damage;

        if (_closestTree.Health <= 0)
        {
            _closestTree.Health = 0;
            TreeList.Trees.Remove(_closestTree);
            
            _lumberState = LumberState.searching;

            return;
        }
    }

    private void LumberSleep()
    {
        // Walk to house and commit suicide
        WalkToClosestHouse();
    }

    private GameObject FindClosestHouse()
    {
        _spawnPoint.SpawnPoints = GameObject.FindGameObjectsWithTag("LumberSpawner");
        float maxDst = Mathf.Infinity;

        foreach (GameObject go in _spawnPoint.SpawnPoints)
        {
            float dst = Vector2.Distance(gameObject.transform.position, go.transform.position);
            if (dst < maxDst)
            {
                _closestHouse = go;
                maxDst = dst;
            }
        }

        return _closestHouse;
    }

    private void WalkToClosestHouse()
    {
        transform.position = Vector2.MoveTowards(transform.position, _closestHouse.transform.position, _speed * Time.deltaTime);
    }

    private float GetDistanceToHouse(GameObject house)
    {
        Vector2 dstVector = transform.position - house.transform.position;
        return dstVector.magnitude;
    }

    private void TimeCycle_OnStartNight(object sender, System.EventArgs e)
    {
        _lumberState = LumberState.sleeping;
    }

    private void AttackBigTree()
    {
        transform.position = Vector2.MoveTowards(transform.position, bigTree.transform.position, _speed * Time.deltaTime);

        if (GetDistanceToHouse(bigTree) < 0.001f)
        {
            _gameManager.EndGame();
        }
    }
}
