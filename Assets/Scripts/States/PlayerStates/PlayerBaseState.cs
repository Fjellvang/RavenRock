using Assets.Scripts.Player_States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.States.PlayerStates
{
	public abstract class PlayerBaseState : BaseState<PlayerController>
	{
		public static readonly PlayerIdleState idleState = new PlayerIdleState();
		public static readonly PlayerMovingState movingState = new PlayerMovingState();
		public static readonly PlayerJumpingState jumpingState = new PlayerJumpingState();
		public static readonly PlayerAttackingState attackingState = new PlayerAttackingState();
		public static readonly PlayerBlockingState blockingState = new PlayerBlockingState();

		protected float inputAxis = 0;
		protected bool jump = false;
		public override void Update(PlayerController controller)
		{
			var jumpPressed = controller.InputHandler.JumpPressed;
			inputAxis = Input.GetAxis("Horizontal") + controller.joystick.Horizontal;
			if (controller.InputHandler.AttackPressed)
			{
				controller.TransitionState(attackingState);
			} else if (controller.InputHandler.BlockPressed)
			{
				controller.TransitionState(blockingState);
			}
			if (jumpPressed && controller.CharacterController.Grounded)
			{
				jump = true;
				controller.TransitionState(jumpingState);
			}
		}

		public override bool OnTakeDamage(PlayerController controller, Vector2 attackDirection)
		{
			controller.health.TakeDmg();
			return true;
		}
	}
}
