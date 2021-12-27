using Assets.Scripts.Enemy.ButcherBoss;
using Assets.Scripts.States.EnemyStates.ButcherBossStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.States
{
	public class ButcherBossStateMachine : StateMachine<ButcherBossBaseState, ButcherBossController>
	{
		public ButcherBossIdleState idleState = new ButcherBossIdleState();
		public ButcherBossWithinRangeState withinRangeState = new ButcherBossWithinRangeState();
		public ButcherBossSearchForShieldState searchForShieldState = new ButcherBossSearchForShieldState();
		public ButcherBossEnrageState enrageState = new ButcherBossEnrageState();
		public ButcherBossThrowingState throwingState = new ButcherBossThrowingState();

		public ButcherBossStateMachine(ButcherBossController controller) : base(controller)
		{
			currentState = idleState;
		}
	}
}
