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
			var jumpPressed = controller.inputState.PressedJump;
			if (controller.inputState.IsPressingAttack)
			{
				controller.StateMachine.TransitionState(attackingState);
			}
			else if (controller.inputState.IsPressingHeavyAttack && controller.combatManager.IsHeavyAttackAvailable) // TODO: add condition
			{
				controller.combatManager.OnHeavyAttack();
				controller.StateMachine.TransitionState(heavyAttackState);
			}
			else if (controller.inputState.IsPressingBlock)
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
