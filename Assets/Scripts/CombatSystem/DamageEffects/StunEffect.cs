using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CombatSystem.DamageEffects
{
	[CreateAssetMenu(fileName = "StunEffect.asset", menuName = "ScriptableObjects/"+nameof(StunEffect)+"ScriptableObject")]
	public class StunEffect : DamageEffect
	{
		public float stunDuration = 1f;
		public override void OnTakeDamage(GameObject defender, GameObject attacker)
		{
			defender.GetComponent<IStunnable>().Stun(stunDuration);
		}
	}
}
