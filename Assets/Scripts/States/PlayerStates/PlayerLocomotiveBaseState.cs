using Assets.Scripts.Player_States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.PlayerStates
{
	/// <summary>
	/// Handles movement.
	/// </summary>
	public abstract class PlayerLocomotiveBaseState : BaseState<PlayerController>
	{
		public static readonly PlayerIdleState idleState = new PlayerIdleState();
		public static readonly PlayerMovingState movingState = new PlayerMovingState();
		public static readonly PlayerJumpingState jumpingState = new PlayerJumpingState();
		//public static readonly PlayerCarryingLadyState playerCarryingState = new PlayerCarryingLadyState();

		float axis = 0;
		bool jump = false;
		public override void Update(PlayerController controller)
		{
			axis = Input.GetAxis("Horizontal");
			var jumpPressed = Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Space);

			if (jumpPressed)
			{
				jump = true;
				controller.TransitionState(jumpingState);
			}

			//if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.E) && controller.Dame.isTouching)
			//{
			//	controller.TransitionState(playerCarryingState);
			//}
			//controller.CharacterController.Move(axis * controller.acceleration * Time.deltaTime, false, jump);
		}

		public override void FixedUpdate(PlayerController controller)
		{
			controller.CharacterController.Move(axis * controller.acceleration * Time.fixedDeltaTime, false, jump);
			jump = false;
		}
	}
}
