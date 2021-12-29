using Assets.Scripts.States.EnemyStates.FarmerStates;

namespace Assets.Scripts.States
{
    public class FarmerStateMachine : StateMachine<EnemyBaseState, FarmerController>
	{
		public FarmerStateMachine(FarmerController controller) : base(controller)
		{
			currentState = movingState;
		}
		//States
		public readonly EnemyAttackingState attackingState = new EnemyAttackingState();
		public readonly EnemyMovingState movingState = new EnemyMovingState();
		public readonly EnemyFleeingState fleeingState = new EnemyFleeingState();
		public readonly EnemyStunnedState stunnedState = new EnemyStunnedState();
		public readonly EnemyDeadState deadState = new EnemyDeadState();

	}
}
