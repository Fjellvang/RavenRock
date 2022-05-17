using UnityEngine;

namespace Assets.Scripts.Algorithms
{
    [CreateAssetMenu(fileName = "QuadtreeBoundary", menuName = "ScriptableObjects/QuadtreeBoundary", order = 1)]
    public class QuadtreeBoundary : ScriptableObject
    {
        public Vector3 TopLeft;
        public Vector3 BottomRight;
    }
}
