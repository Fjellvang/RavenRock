using Assets.Scripts.Android_UI;
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
			controller.Animator.Play("Blocking");
		}

		public override void Update(PlayerController controller)
		{
			inputAxis = 0;
			if (!AndroidButtonHandler.Instance.BlockPressed)
			{
				controller.PoplastState();
			}
		}

		public override void OnExitState(PlayerController controller)
		{
		}

		public override bool OnTakeDamage(PlayerController controller, Vector2 attackDirection)
		{
			bool attackFromLeft = attackDirection.x > 0;
			bool facingRight = controller.CharacterController.FacingRight;
			if (attackFromLeft && facingRight || !attackFromLeft && !facingRight)
			{
				//We are attacked in the back
				controller.health.TakeDmg();
				return true;
			}
			controller.health.Block();
			return false;

		}
	}
}
