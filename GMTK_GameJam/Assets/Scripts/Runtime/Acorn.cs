using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : MonoBehaviour
{
    public Vector3 goToPosition;

    [SerializeField] GameObject tree;
    private TreeBehaviour _treeBehaviour;

    private void Start()
    {
        _treeBehaviour = FindObjectOfType<TreeBehaviour>();       
    }

    private void Update()
    {
        if ((transform.position - goToPosition).magnitude < 0.1)
        {
            if (_treeBehaviour._nutSource != null)
            {
                _treeBehaviour._nutSource.clip = _treeBehaviour._landNut;
                _treeBehaviour._nutSource.Play();
            }
            Instantiate(tree, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
