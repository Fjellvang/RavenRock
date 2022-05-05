using Assets.Scripts.Player_States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.EnemyStates.FarmerStates
{
	public class EnemyMovingState : EnemyBaseState
	{
		//float directionalForce = 0;
		public override void OnEnterState(FarmerController controller)
		{
			controller.Animator.Play("Running");
		}

		public override void OnExitState(FarmerController controller)
		{
		}

		public override void Update(FarmerController controller)
		{
			if (!controller.playerVisible)
			{
				return;
			}

			var vectorTowardsPlayer = controller.player.transform.position - controller.transform.position;
			var direction = vectorTowardsPlayer.x > 0 ? 1 : -1;
			directionalForce = direction * controller.moveSpeed;
			bool withinRange = controller.weapon.WithinRange(controller.player.transform);
			if (withinRange)
			{
				controller.stateMachine.TransitionState(controller.stateMachine.attackingState);
			}
		}

	}
}
