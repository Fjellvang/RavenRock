using Assets.Scripts.Enemy.ButcherBoss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.EnemyStates.ButcherBossStates
{
	public class ButcherBossIdleState : ButcherBossBaseState
	{
		public override void OnEnterState(ButcherBossController controller)
		{
			//controller.animator.Play("ButcherBossShieldBash");
			controller.animator.Play("Idle");
		}

		public override void OnExitState(ButcherBossController controller)
		{
			controller.animator.Play("Idle");
		}

		public override void Update(ButcherBossController controller)
		{
			var targetDelta = (controller.projectileTarget.position.x - controller.transform.position.x);
			var direction = targetDelta > 1 ? 1 : -1;
			directionalForce = direction;

			var inRange = controller.weapon.WithinRange(controller.player.transform);
			if (inRange)
			{
				controller.stateMachine.TransitionState(controller.stateMachine.withinRangeState);
			}
			if (Mathf.Abs(targetDelta) > controller.projectileRange)
			{
				controller.stateMachine.TransitionState(controller.stateMachine.throwingState);
			}
		}
	}
}
