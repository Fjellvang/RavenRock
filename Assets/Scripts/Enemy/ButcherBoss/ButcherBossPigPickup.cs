using Assets.Scripts.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy.ButcherBoss
{
    public class ButcherBossPigPickup : MonoBehaviour
	{
		private PigPickupManager pickupManager;
		[Inject]
		public void Construct(PigPickupManager settings)
		{
			pickupManager = settings;
		}
        private void Start()
        {
            if (pickupManager.quadTree == null)
            {
				Debug.LogWarning("QUADTREE HAST NOT BEEN INITIALZED FOR PICKUP");
				return;
            }

			var p = new Point(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
			pickupManager.quadTree.Insert(new Node<ButcherBossPigPickup>(this, p));
            
        }
        public void OnPickup()
		{
			Destroy(gameObject);
		}
	}
}
