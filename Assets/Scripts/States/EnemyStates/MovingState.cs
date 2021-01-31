using Assets.Scripts.Player_States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.EnemyStates
{
	public class MovingState : EnemyBaseState
	{
		float directionalForce = 0;
		public override void OnEnterState(AI controller)
		{
		}

		public override void OnExitState(AI controller)
		{
		}

		public override void Update(AI controller)
		{
			if (!controller.witinRange)
			{
				return;
			}

			var vectorTowardsPlayer = controller.player.transform.position - controller.transform.position;
			var direction = vectorTowardsPlayer.x > 0 ? 1 : -1;

			directionalForce = direction * controller.moveSpeed;
			Debug.DrawRay(controller.transform.position, new Vector3(direction, 0), Color.red);
			if (controller.weapon.withinRange)
			{
				controller.TransitionState(controller.attackingState);
			}
		}

		public override void FixedUpdate(AI controller)
		{
			controller.controller.Move(directionalForce * Time.fixedDeltaTime, false, false);
		}
	}
}
