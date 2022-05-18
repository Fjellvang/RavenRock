using Assets.Scripts.Algorithms;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy.ButcherBoss
{
    public class ButcherScenePickupManager : MonoBehaviour
    {
        [ExposedScriptableObject]
		public QuadtreeBoundary boundary;

		[Inject]
		public void Construct(PigPickupManager settings)
        {
            if (boundary == null)
            {
				Debug.LogWarning("Quadsrettings will not be initialized correcly!");
            }

			//initializing a singleton from a constructor is probably not that cool...
			var topLeft = new Point(Mathf.RoundToInt(boundary.TopLeft.x), Mathf.RoundToInt(boundary.TopLeft.y));
			var bottomRight = new Point(Mathf.RoundToInt(boundary.BottomRight.x), Mathf.RoundToInt(boundary.BottomRight.y));
			settings.quadTree = new QuadTree<ButcherBossPigPickup>(topLeft, bottomRight);	
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(boundary.TopLeft, 2);
            Gizmos.DrawSphere(boundary.BottomRight, 2);
        }
    }
}
