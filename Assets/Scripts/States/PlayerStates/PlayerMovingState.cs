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
			controller.playerSettings.StaminaMultiplier = controller.staminaMovingMultiplier;
			controller.playerState.IsRunning = true;
		}

		public override void OnExitState(PlayerController controller)
		{
			controller.playerState.IsRunning = false;
		}

		public override void Update(PlayerController controller)
		{
			base.Update(controller);
			if (Mathf.Abs(controller.CharacterController.Velocity.x) <= 0.1f)
			{
				controller.StateMachine.TransitionState(idleState);
			}
		}
	}
}
