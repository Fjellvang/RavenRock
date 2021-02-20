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
			var diceroll = UnityEngine.Random.Range(0f, 1f);
			if (diceroll > .6f)
			{
				controller.Animator.Play("Attack2");
				return;
			}
			controller.Animator.Play("Attack");
		}

		public override void Update(PlayerController controller)
		{
			inputAxis = 0;
			var state = controller.Animator.GetCurrentAnimatorStateInfo(0);
			var isNotAttacking = !state.IsName("Attack") && !state.IsName("Attack2");
			if (isNotAttacking)
			{
				controller.PoplastState();
			}
		}

		public override void OnExitState(PlayerController controller)
		{
		}
	}
}
