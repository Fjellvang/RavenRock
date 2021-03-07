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
			//controller.bossHealth.TakeDamage(attacker); //TODO: This asumes player never gets behind the boss.. FIX
			for (int i = 0; i < attackEffects.Length; i++)
			{
				//TODO: This asumes player never gets behind the boss.. FIX
				attackEffects[i].OnSuccessFullAttack(attacker, controller.bossHealth.gameObject);
			}
		}
	}
}
