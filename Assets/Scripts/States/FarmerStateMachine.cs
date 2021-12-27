using Assets.Scripts.States.EnemyStates.FarmerStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.States
{
	public class FarmerStateMachine : StateMachine<EnemyBaseState, AI>
	{
		public FarmerStateMachine(AI controller) : base(controller)
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
