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

		public override void Update(PlayerController controller)
		{
			var axis = Input.GetAxis("Horizontal");
			var jump = Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Space);

			if (jump)
			{
				controller.TransitionState(controller.jumpingState);
			}
			if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.E) && controller.Dame.isTouching)
			{
				controller.TransitionState(controller.playerCarryingState);
			}
			controller.CharacterController.Move(axis * controller.acceleration * Time.deltaTime, false, jump);
		}
	}
}
