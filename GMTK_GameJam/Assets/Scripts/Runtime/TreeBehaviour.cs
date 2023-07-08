using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite[] treeSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float timeToLevel1;
    [SerializeField] private float timeToLevel2;

    [SerializeField] private int treeLevel = 0;

    private float timeOfPlanting;

    private bool isPlanted = false;

    private void Update()
    {
        // Only runs once when planted
        if (!isPlanted)
        {
            TreeList.Trees.Add(gameObject);
            timeOfPlanting = Time.time;
            SetSortingLayer();
            isPlanted = true;
        }

        GrowTree();
    }

    private void UpdateTreeSprite()
    {
        spriteRenderer.sprite = treeSprites[treeLevel];
    }

    private void GrowTree()
    {
        if (Time.time - timeOfPlanting >= timeToLevel1 && Time.time - timeOfPlanting < timeToLevel2)
        {
            treeLevel = 1;
            UpdateTreeSprite();
        }
        else if (Time.time - timeOfPlanting >= timeToLevel2)
        {
            treeLevel = 2;
            UpdateTreeSprite();
        }

    }

    private void SetSortingLayer()
    {
        int order = -(int)(transform.position.y * 100);
        spriteRenderer.sortingOrder = order;
    }
}
