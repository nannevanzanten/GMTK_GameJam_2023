using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : MonoBehaviour
{
    public Vector3 goToPosition;

    [SerializeField] GameObject tree;

    private void Update()
    {
        if ((transform.position - goToPosition).magnitude < 0.1)
        {
            //Instantiate(tree, transform);
            Instantiate(tree, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
