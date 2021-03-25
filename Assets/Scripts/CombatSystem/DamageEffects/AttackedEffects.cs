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

		public void OnDamage(GameObject attacker)
		{
			ApplyDamageEffects(damageEffects, attacker);
		}

		public void OnCriticalDamage(GameObject attacker)
		{
			ApplyDamageEffects(criticalDamageEffects, attacker);
		}

		private void ApplyDamageEffects(IDamageEffect[] effects, GameObject attacker)
		{
			for (int i = 0; i < effects.Length; i++)
			{
				effects[i].OnTakeDamage(gameObject, attacker);
			}
		}
	}
}
