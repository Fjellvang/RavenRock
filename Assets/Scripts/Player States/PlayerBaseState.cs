using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player_States
{
	public abstract class PlayerBaseState : BaseState<PlayerController>
	{

		float axis = 0;
		bool jump = false;
		public override void Update(PlayerController controller)
		{
			axis = Input.GetAxis("Horizontal");
			var jumpPressed = Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Space);

			if (jumpPressed)
			{
				jump = true;
				controller.TransitionState(controller.jumpingState);
			}
			if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.E) && controller.Dame.isTouching)
			{
				controller.TransitionState(controller.playerCarryingState);
			}
			//controller.CharacterController.Move(axis * controller.acceleration * Time.deltaTime, false, jump);
		}

		public override void FixedUpdate(PlayerController controller)
		{
			controller.CharacterController.Move(axis * controller.acceleration * Time.fixedDeltaTime, false, jump);
			jump = false;
		}
	}
}
