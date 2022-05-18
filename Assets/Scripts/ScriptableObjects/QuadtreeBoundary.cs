using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "QuadtreeBoundary", menuName = "ScriptableObjects/QuadtreeBoundary", order = 1)]
    public class QuadtreeBoundary : ScriptableObject
    {
        public Vector3 TopLeft;
        public Vector3 BottomRight;
    }
}
