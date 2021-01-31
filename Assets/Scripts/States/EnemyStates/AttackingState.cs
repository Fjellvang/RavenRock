using Assets.Scripts.Player_States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.States.EnemyStates
{
	public class AttackingState : EnemyBaseState
	{
		public override void OnEnterState(AI controller)
		{
            controller.Animator.SetBool("inRange", true);
		}

		public override void OnExitState(AI controller)
		{
            controller.Animator.SetBool("inRange", false);
		}

		public override void Update(AI controller)
		{
			if (!controller.weapon.withinRange)
			{
				controller.TransitionState(controller.movingState);
			}
		}
	}
}
