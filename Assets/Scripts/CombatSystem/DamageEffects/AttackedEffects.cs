using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CombatSystem.DamageEffects
{
	public class AttackedEffects : MonoBehaviour
	{
		public DamageEffect[] damageEffects;
		public DamageEffect[] criticalDamageEffects;

		public void OnDamage(GameObject attacker)
		{
			ApplyDamageEffects(damageEffects, attacker);
		}

		public void OnCriticalDamage(GameObject attacker)
		{
			ApplyDamageEffects(criticalDamageEffects, attacker);
		}

		private void ApplyDamageEffects(DamageEffect[] effects, GameObject attacker)
		{
			for (int i = 0; i < effects.Length; i++)
			{
				effects[i].OnTakeDamage(gameObject, attacker);
			}
		}
	}
}
