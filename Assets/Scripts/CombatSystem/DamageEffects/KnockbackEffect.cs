using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CombatSystem.DamageEffects
{
	[CreateAssetMenu(fileName = "KnockbackEffect.asset", menuName = "ScriptableObjects/KnockbackEffectScriptableObject", order = 1)]
	public class KnockbackEffect : DamageEffect
	{
		public float knockbackStrength = 5f;
		public float yForce = 2f;
		public override void OnTakeDamage(GameObject defender, GameObject attacker)
		{
			var delta = defender.transform.position - attacker.transform.position;
			delta.y += yForce;
			defender.GetComponent<Rigidbody2D>().AddForce(delta * knockbackStrength);
		}
	}
}
