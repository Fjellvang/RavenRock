using Assets.Scripts.Enemy.ButcherBoss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.States.EnemyStates.ButcherBossStates
{
	public class ButcherBossWithinRangeState : ButcherBossBaseState
	{
		public override void OnEnterState(ButcherBossController controller)
		{
			directionalForce = 0;
			controller.animator.Play("ShieldBashCooldown");
		}

		public override void OnExitState(ButcherBossController controller)
		{
		}

		public override void Update(ButcherBossController controller)
		{
			var outOfRange = !controller.weapon.WithinRange(controller.player.transform);
			if (outOfRange)
			{
				controller.stateMachine.TransitionState(controller.stateMachine.idleState);
			}
		}
	}
}
