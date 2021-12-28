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
	public abstract class PlayerLocomotiveBaseState : PlayerBaseState
	{
		public override void Update(PlayerController controller)
		{
			base.Update(controller);
			var jumpPressed = Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Space);
			inputAxis = Input.GetAxis("Horizontal");
			if (Input.GetButtonDown("Attack"))
			{
				controller.StateMachine.TransitionState(attackingState);
			}
			else if (Input.GetButton("Block"))
			{
				controller.StateMachine.TransitionState(blockingState);
			}
			if (jumpPressed && controller.CharacterController.Grounded)
			{
				jump = true;
				controller.StateMachine.TransitionState(jumpingState);
			}
		}
		public override void FixedUpdate(PlayerController controller)
		{
			controller.CharacterController.Move(inputAxis * controller.acceleration * Time.fixedDeltaTime, false, jump);
			jump = false;
		}
	}
}
