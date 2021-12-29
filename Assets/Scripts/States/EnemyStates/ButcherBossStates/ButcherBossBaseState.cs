using Assets.Scripts.CombatSystem;
using Assets.Scripts.Enemy.ButcherBoss;
using Assets.Scripts.Player_States;
using UnityEngine;

namespace Assets.Scripts.States.EnemyStates.ButcherBossStates
{
    public abstract class ButcherBossBaseState : BaseState<ButcherBossController>
	{
		protected float directionalForce = 0;
		//TODO: This is identical to to enemy base state.... Refactor.
		public override void FixedUpdate(ButcherBossController controller)
		{
			controller.controller.Move(directionalForce * controller.movementSpeed * Time.fixedDeltaTime, false, false);
		}

		public override void OnTakeDamage(ButcherBossController controller, GameObject attacker, IAttackEffect[] attackEffects)
		{
			for (int i = 0; i < attackEffects.Length; i++)
			{
				//TODO: This asumes player never gets behind the boss.. FIX
				attackEffects[i].OnSuccessFullAttack(attacker, controller.meatShieldHealth.gameObject);
			}
		}
	}
}
