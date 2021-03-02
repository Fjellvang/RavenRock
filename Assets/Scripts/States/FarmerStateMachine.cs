using Assets.Scripts.States.EnemyStates;
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
		public readonly AttackingState attackingState = new AttackingState();
		public readonly MovingState movingState = new MovingState();
		public readonly EnemyStunnedState stunnedState = new EnemyStunnedState();
		public readonly EnemyDeadState deadState = new EnemyDeadState();

	}
}
