using Assets.Scripts.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy.ButcherBoss
{
	public class ButcherCleaver : MonoBehaviour, IAttackable
	{
		public Rigidbody2D rig;

		public void OnTakeDamage(GameObject attacker, IAttackEffect[] attackEffects)
		{

			//for (int i = 0; i < attackedEffects.Length; i++)
			//{
			//	attackedEffects[i].OnTakeDamage(this.gameObject, attacker);
			//}
			var delta = transform.position - attacker.transform.position;
			rig.AddForce(delta * 20, ForceMode2D.Impulse);
		}

		public void PowerFullAttack()
		{
			throw new NotImplementedException();
		}

		private void Awake()
		{
			rig = GetComponent<Rigidbody2D>();
		}

		private IAttackEffect[] attackEffects = new IAttackEffect[]
		{
			new ButcherCleaverAttack()
		};
		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Attack"))
			{
				return;
			}
			if (collision.gameObject.TryGetComponent<IAttackable>(out var attackable))
			{
				attackable.OnTakeDamage(gameObject, attackEffects);
			}
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
