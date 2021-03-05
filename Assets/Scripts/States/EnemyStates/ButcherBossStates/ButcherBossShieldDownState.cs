using Assets.Scripts.CombatSystem;
using Assets.Scripts.Enemy.ButcherBoss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.EnemyStates.ButcherBossStates
{
	public abstract class ButcherBossShieldDownState : ButcherBossBaseState
	{
		public override void OnTakeDamage(ButcherBossController controller, GameObject attacker, IAttackEffect[] attackEffects)
		{
			controller.meatShieldHealth.TakeDamage(Vector3.zero); //TODO: This asumes player never gets behind the boss.. FIX
		}
	}
}
