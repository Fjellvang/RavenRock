using Assets.Scripts.CombatSystem;
using Assets.Scripts.Player_States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.PlayerStates
{
	public abstract class PlayerBaseState : BaseState<PlayerController>
	{
		public static readonly PlayerIdleState idleState = new PlayerIdleState();
		public static readonly PlayerMovingState movingState = new PlayerMovingState();
		public static readonly PlayerJumpingState jumpingState = new PlayerJumpingState();
		public static readonly PlayerAttackingState attackingState = new PlayerAttackingState();
		public static readonly PlayerBlockingState blockingState = new PlayerBlockingState();
		public static readonly PlayerStunnedState stunnedState = new PlayerStunnedState();

		protected float inputAxis = 0;
		protected bool jump = false;
		public override void Update(PlayerController controller)
		{
			var jumpPressed = Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Space);
			inputAxis = Input.GetAxis("Horizontal");
			if (Input.GetButtonDown("Attack"))
			{
				controller.StateMachine.TransitionState(attackingState);
			} else if (Input.GetButton("Block"))
			{
				controller.StateMachine.TransitionState(blockingState);
			}
			if (jumpPressed && controller.CharacterController.Grounded)
			{
				jump = true;
				controller.StateMachine.TransitionState(jumpingState);
			}
		}

		public override void OnTakeDamage(PlayerController controller, GameObject attacker, IAttackEffect[] attackEffects)
		{
			for (int i = 0; i < attackEffects.Length; i++)
			{
				attackEffects[i].OnSuccessFullAttack(attacker, controller.gameObject);
			}
		}
	}
}
