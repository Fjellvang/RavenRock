using Assets.Scripts.Algorithms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTest : MonoBehaviour
{
    public Transform TopLeft;
    public Transform BottomRight;

    public GameObject[] ObjectsInTree;

    QuadTree<GameObject> tree;

    public Transform ToCheckFrom;

    private Point ToPoint(Vector3 vector)
    {
        return new Point(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
    }

    private void OnEnable()
    {
        tree = new QuadTree<GameObject>(ToPoint(TopLeft.position), ToPoint(BottomRight.position));
        foreach (var item in ObjectsInTree)
        {
            tree.Insert(new Node<GameObject>(item, ToPoint(item.transform.position))); // now we loosing presicion. FIX
        }

    }
    // Update is called once per frame
    void Update()
    {
        var test = (d: 9999999f, node: new Node<GameObject>());
        var nearest = tree.FindNearest(ToPoint(ToCheckFrom.position), ref test, tree);

        Debug.DrawLine(ToCheckFrom.position, nearest.Data.transform.position);
    }
}
