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
		public override void OnEnterState(PlayerController controller)
		{
			controller.Animator.enabled = false;
		}

		public override void Update(PlayerController controller)
		{
			base.Update(controller);
			if (controller.CharacterController.Grounded)
			{
				Debug.Log("GROUNDED");
				controller.PoplastState();
			}
		}

		public override void OnExitState(PlayerController controller)
		{
			controller.Animator.enabled = true;
		}

	}
}
