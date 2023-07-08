using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeList : MonoBehaviour
{
    public static List<GameObject> Trees;

    private void Awake()
    {
        Trees = new List<GameObject>();
    }
}
