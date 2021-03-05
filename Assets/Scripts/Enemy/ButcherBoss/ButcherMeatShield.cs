using Assets.Scripts.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy.ButcherBoss
{
	[RequireComponent(typeof(Health))]
	public class ButcherMeatShield : MonoBehaviour, IAttackable
	{
		public void OnTakeDamage(GameObject attacker, IAttackEffect[] attackEffects)
		{

		}
	}
}
