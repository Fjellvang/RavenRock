using Assets.Scripts.Enemy.ButcherBoss;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ButcherScenePickupManager))]
public class ButcherScenePickupManagerEditor : Editor
{
    Transform topLeft = null;
    Transform bottomRight = null;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var manager = (ButcherScenePickupManager)target;

        topLeft = EditorGUILayout.ObjectField("Top Left", topLeft, typeof(Transform), true) as Transform;
        bottomRight = EditorGUILayout.ObjectField("Bottom Right", bottomRight, typeof(Transform), true) as Transform;

        if (topLeft != null)
        {
            manager.boundary.TopLeft = topLeft.position;
        }
        if (bottomRight != null)
        {
            manager.boundary.BottomRight = bottomRight.position;
        }
    }
}
