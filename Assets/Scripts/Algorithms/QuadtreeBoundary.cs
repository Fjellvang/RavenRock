using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
