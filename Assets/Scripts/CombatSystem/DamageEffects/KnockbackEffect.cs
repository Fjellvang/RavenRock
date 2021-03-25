using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CombatSystem.DamageEffects
{
	public class KnockbackEffect : MonoBehaviour, IDamageEffect
	{
		public float knockbackStrength = 5f;
		public float yForce = 2f;
		public bool critOnly;

		public bool CriticalOnly() => critOnly;

		public void OnTakeDamage(GameObject defender, GameObject attacker)
		{
			var delta = defender.transform.position - attacker.transform.position;
			delta.y += yForce;
			defender.GetComponent<Rigidbody2D>().AddForce(delta * knockbackStrength);
		}
	}
}
