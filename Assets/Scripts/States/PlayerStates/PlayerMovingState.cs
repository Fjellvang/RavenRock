using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.PlayerStates
{
	public class PlayerMovingState : PlayerLocomotiveBaseState
	{
		public override void OnEnterState(PlayerController controller)
		{
			controller.Animator.Play("Run");
		}

		public override void OnExitState(PlayerController controller)
		{
		}

		public override void Update(PlayerController controller)
		{
			base.Update(controller);
			if (Mathf.Abs(controller.CharacterController.Velocity.x) <= 0.1f)
			{
				controller.Animator.SetBool("isMoving", false);
				controller.TransitionState(idleState);
			}
		}
	}
}
