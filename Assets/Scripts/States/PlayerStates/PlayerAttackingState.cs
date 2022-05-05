using UnityEngine;

namespace Assets.Scripts.States.PlayerStates
{
    public class PlayerAttackingState : PlayerLocomotiveBaseState
	{
		float pressedAttackGrace = 0.1f; //TODO: move to somewhere modifyable?
		float timeSinceLastAttackPress = 0;
		public override void OnEnterState(PlayerController controller)
		{
			controller.Animator.Play("Attack");

			controller.playerSettings.StaminaMultiplier = controller.staminaFightingMultiplier;

		}

		public override void Update(PlayerController controller)
		{
			inputAxis = 0;
			timeSinceLastAttackPress -= Time.deltaTime;
			var pressingAttack = controller.inputState.IsPressingAttack;
            if (pressingAttack)
            {
				timeSinceLastAttackPress = pressedAttackGrace;
            }
			var attacking = timeSinceLastAttackPress > 0;
			var isNotAttacking = !attacking && controller.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9;
			if (isNotAttacking)
			{
				controller.StateMachine.PoplastState();
			}
		}

		public override void OnExitState(PlayerController controller)
		{
		}
	}
}
