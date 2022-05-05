using Assets.Scripts.Enemy.ButcherBoss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.EnemyStates.ButcherBossStates
{
	public class ButcherBossSearchForShieldState : ButcherBossShieldDownState
	{
		private ButcherBossPigPickup target;
		public override void OnEnterState(ButcherBossController controller)
		{
			target = controller.remainingShields.First();
			controller.remainingShields.Remove(target);
		}

		public override void OnExitState(ButcherBossController controller)
		{
			controller.meatShieldHealth.gameObject.SetActive(true);
			controller.meatShieldHealth.ResetHealth();
		}

		public override void Update(ButcherBossController controller)
		{
			var deltaX = (target.transform.position.x - controller.transform.position.x);
			var direction = deltaX > 1 ? 1 : -1;
			directionalForce = direction;

			if (Mathf.Abs(deltaX) <= 1) //TODO: unhardcode.
			{
				target.OnPickup();
				controller.stateMachine.TransitionState(controller.stateMachine.idleState);
			}
		}
	}
}
