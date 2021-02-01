using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.PlayerStates
{
	public class PlayerBlockingState : PlayerLocomotiveBaseState
	{
		public override void OnEnterState(PlayerController controller)
		{
			controller.Animator.SetBool("blockButton", true);
		}

		public override void Update(PlayerController controller)
		{
			inputAxis = 0;
			if (!Input.GetButton("Block"))
			{
				controller.PoplastState();
			}
		}

		public override void OnExitState(PlayerController controller)
		{
			controller.Animator.SetBool("blockButton", false);
		}
	}
}
