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
		public static readonly PlayerExhaustedState exhaustedState = new PlayerExhaustedState();
		public static readonly PlayerMovingState movingState = new PlayerMovingState();
		public static readonly PlayerJumpingState jumpingState = new PlayerJumpingState();
		public static readonly PlayerAttackingState attackingState = new PlayerAttackingState();
		public static readonly PlayerHeavyAttackState heavyAttackState = new PlayerHeavyAttackState();
		public static readonly PlayerBlockingState blockingState = new PlayerBlockingState();
		public static readonly PlayerStunnedState stunnedState = new PlayerStunnedState();

		protected float inputAxis = 0;
		protected bool jump = false;
		public override void Update(PlayerController controller)
		{
			controller.playerState.IsGrounded = controller.CharacterController.Grounded;
			inputAxis = controller.inputState.HorizontalMovement;
            if (controller.combatManager.IsHeavyAttackAvailable)
            {
				controller.playerRenderer.materials = controller.HeavyAttackReady;
            }
            else
            {
				controller.playerRenderer.materials = controller.noHeavyAttackReady;
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
