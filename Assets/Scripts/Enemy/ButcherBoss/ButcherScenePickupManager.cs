using Assets.Scripts.Algorithms;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy.ButcherBoss
{
    public class ButcherScenePickupManager : MonoBehaviour
    {
		public Transform TopLeft;
		public Transform BottomRight;

		[Inject]
		public void Construct(PigPickupManager settings)
        {
            if (TopLeft == null || BottomRight == null)
            {
				Debug.LogWarning("Quadsrettings will not be initialized correcly!");
            }

			//initializing a singleton from a constructor is probably not that cool...
			var topLeft = new Point(Mathf.RoundToInt(TopLeft.position.x), Mathf.RoundToInt(TopLeft.position.y));
			var bottomRight = new Point(Mathf.RoundToInt(BottomRight.position.x), Mathf.RoundToInt(BottomRight.position.y));
			settings.quadTree = new QuadTree<ButcherBossPigPickup>(topLeft, bottomRight);	
        }
	}
}
