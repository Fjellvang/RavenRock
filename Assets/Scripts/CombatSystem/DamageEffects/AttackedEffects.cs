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
		public IDamageEffect[] damageEffects;
		public IDamageEffect[] criticalDamageEffects;
		//IDamageEffect
		private void Awake()
		{
			criticalDamageEffects = GetComponentsInChildren<IDamageEffect>();
			damageEffects = criticalDamageEffects.Where(x => !x.CriticalOnly()).ToArray();
		}

		public void OnDamage(GameObject attacker, float damage)
		{
			ApplyDamageEffects(damageEffects, attacker, damage);
		}

		public void OnCriticalDamage(GameObject attacker, float damage)
		{
			ApplyDamageEffects(criticalDamageEffects, attacker, damage);
		}

		private void ApplyDamageEffects(IDamageEffect[] effects, GameObject attacker, float damage)
		{
			for (int i = 0; i < effects.Length; i++)
			{
				effects[i].OnTakeDamage(attacker, damage);
			}
		}
	}
}
