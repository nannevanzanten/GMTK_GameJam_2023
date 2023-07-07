using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackSpawnPoint : MonoBehaviour
{
    [SerializeField] private LumberjackBehaviour spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(spawnPoint, gameObject.transform.position);
    }
}
