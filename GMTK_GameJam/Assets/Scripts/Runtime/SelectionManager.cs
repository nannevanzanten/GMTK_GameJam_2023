using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public event EventHandler OnSelectTree;

    //private GameObject selectedTree;
    [SerializeField] private TreeBehaviour selectedTree;

    private void Update()
    {
        RaycastHit2D? focusedTree = GetFocusedTree();

        if (focusedTree.HasValue && Input.GetMouseButtonDown(0))
        {
            selectedTree = focusedTree.Value.collider.gameObject.GetComponent<TreeBehaviour>();
            OnSelectTree?.Invoke(this, EventArgs.Empty);
        }
    }

    private RaycastHit2D? GetFocusedTree()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit == true && (hit.collider.gameObject.tag == "tree" || hit.collider.gameObject.tag == "BigTree"))
        {
            return hit;
        }

        return null;
    }

    public TreeBehaviour GetSelectedTree()
    {
        return selectedTree;
    }
}
