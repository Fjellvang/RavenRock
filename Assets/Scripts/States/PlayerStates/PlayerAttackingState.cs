using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.States.PlayerStates
{
	public class PlayerAttackingState : PlayerBaseState
	{
		public override void OnEnterState(PlayerController controller)
		{
			inputAxis = 0;
			controller.Animator.Play("Attack");
		}

		public override void Update(PlayerController controller)
		{
			var isNotAttacking = !controller.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
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
