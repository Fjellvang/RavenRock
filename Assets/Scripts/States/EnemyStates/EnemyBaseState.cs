using Assets.Scripts.Player_States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.States.EnemyStates
{
	public class EnemyBaseState : BaseState<AI>
	{
		protected float directionalForce = 0;
		public override void OnEnterState(AI controller)
		{
		}

		public override void OnExitState(AI controller)
		{
		}

		public override void Update(AI controller)
		{
		}
	}
}
