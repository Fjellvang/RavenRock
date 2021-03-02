using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.PlayerStates
{
	public class PlayerAttackingState : PlayerLocomotiveBaseState
	{
		public override void OnEnterState(PlayerController controller)
		{
			controller.Animator.Play("Attack");
			//controller.Animator.SetBool("shootButton", true);
		}

		public override void Update(PlayerController controller)
		{
			inputAxis = 0;
			var isNotAttacking = !Input.GetButton("Attack");
			if (isNotAttacking)
			{
				controller.StateMachine.PoplastState();
			}
		}

		public override void OnExitState(PlayerController controller)
		{
			//controller.Animator.SetBool("shootButton", false);
		}
	}
}
