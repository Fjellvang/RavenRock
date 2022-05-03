namespace Assets.Scripts.States.PlayerStates
{
    public class PlayerHeavyAttackState : PlayerLocomotiveBaseState
	{
		public override void OnEnterState(PlayerController controller)
		{
			controller.Animator.Play("Attack3");

			controller.playerSettings.StaminaMultiplier = controller.staminaFightingMultiplier;

		}

		public override void Update(PlayerController controller)
		{
			inputAxis = 0;
			var isNotAttacking = controller.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9;
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
