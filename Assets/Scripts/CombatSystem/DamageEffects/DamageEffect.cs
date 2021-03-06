using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CombatSystem.DamageEffects
{
	public abstract class DamageEffect : ScriptableObject
	{
		public abstract void OnTakeDamage(GameObject defender, GameObject attacker);
	}
}
