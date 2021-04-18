using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CombatSystem.DamageEffects
{
	public class StunEffect : MonoBehaviour, IDamageEffect
	{
		public float stunDuration = 1f;
		public bool critOnly;

		public bool CriticalOnly() => critOnly;

		private IStunnable stunnable;

		private void Awake()
		{
			stunnable = GetComponent<IStunnable>();
		}

		public void OnTakeDamage(GameObject defender, GameObject attacker)
		{
			stunnable.Stun(stunDuration);
		}
	}
}
