using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeList : MonoBehaviour
{
    public static List<TreeBehaviour> Trees;

    private void Awake()
    {
        Trees = new List<TreeBehaviour>();
    }
}
