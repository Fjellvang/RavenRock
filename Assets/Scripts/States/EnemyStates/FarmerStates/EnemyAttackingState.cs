namespace Assets.Scripts.States.EnemyStates.FarmerStates
{
    public class EnemyAttackingState : EnemyBaseState
	{
		public override void OnEnterState(FarmerController controller)
		{
			controller.Animator.Play("Attacking");
		}

		public override void OnExitState(FarmerController controller)
		{
		}

		public override void Update(FarmerController controller)
		{
			//TODO: Fix this. maybe add animation event or something better?
			var inAttack = controller.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < .9f;
			var outOfRange = !controller.weapon.WithinRange(controller.player.transform);
			if (outOfRange && !inAttack)
			{
				controller.StateMachine.TransitionState(controller.StateMachine.movingState);
			}
            if (controller.weapon.successfullyAttacked)
            {
				controller.StateMachine.TransitionState(controller.StateMachine.fleeingState);
            }
		}
	}
}
