using Assets.Scripts.Player_States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.EnemyStates.FarmerStates
{
	public class MovingState : EnemyBaseState
	{
		//float directionalForce = 0;
		public override void OnEnterState(AI controller)
		{
			controller.Animator.Play("Running");
		}

		public override void OnExitState(AI controller)
		{
		}

		public override void Update(AI controller)
		{
			if (!controller.playerVisible)
			{
				return;
			}

			var vectorTowardsPlayer = controller.player.transform.position - controller.transform.position;
			var direction = vectorTowardsPlayer.x > 0 ? 1 : -1;

			directionalForce = direction * controller.moveSpeed;
			bool withinRange = IsPlayerInAttackRange(controller);
			if (withinRange)
			{
				controller.StateMachine.TransitionState(controller.StateMachine.attackingState);
			}
		}

	}
}
