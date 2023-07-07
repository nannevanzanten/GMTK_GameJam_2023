using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : MonoBehaviour
{
    public Vector3 goToPosition;

    private void Update()
    {
        if ((transform.position - goToPosition).magnitude < 0.1)
        {
            Destroy(gameObject);
        }
    }
}
