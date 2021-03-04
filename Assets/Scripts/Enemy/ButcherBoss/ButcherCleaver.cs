using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy.ButcherBoss
{
	public class ButcherCleaver : MonoBehaviour
	{
		public Rigidbody2D rig;
		private void Awake()
		{
			rig = GetComponent<Rigidbody2D>();
		}
		private void OnCollisionEnter2D(Collision2D collision)
		{
			rig.simulated = false;
			GetComponent<CircleCollider2D>().enabled = false;
			Destroy(gameObject, 1);
		}

		private void Update()
		{
			var vel = -rig.velocity.normalized;
			var angle = Mathf.Atan2(vel.y, vel.x);
			transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
		}
	}
}
