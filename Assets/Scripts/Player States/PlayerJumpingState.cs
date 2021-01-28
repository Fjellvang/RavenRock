using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player_States
{
	public class PlayerJumpingState : PlayerBaseState
	{
		PlayerBaseState lastState;
		public override void OnEnterState(PlayerController controller, BaseState<PlayerController> transitionFrom)
		{
			controller.Animator.enabled = false;
			lastState = transitionFrom as PlayerBaseState;
		}

		public override void Update(PlayerController controller)
		{
			base.Update(controller);
			if (controller.CharacterController.Grounded)
			{
				Debug.Log("GROUNDED");
				controller.TransitionState(lastState);
			}
		}

		public override void OnExitState(PlayerController controller)
		{
			controller.Animator.enabled = true;
		}

	}
}
