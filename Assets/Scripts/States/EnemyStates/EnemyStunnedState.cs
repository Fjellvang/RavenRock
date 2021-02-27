using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.EnemyStates
{
	public class EnemyStunnedState : EnemyBaseState
	{
		float timer = 0;
		public override void OnEnterState(AI controller)
		{
			controller.Animator.Play("Stunned");
			timer = 0;
		}

		public override void Update(AI controller)
		{
			timer += Time.deltaTime;
			if (timer > controller.stunnedDuration)
			{
				controller.TransitionState(controller.movingState);
			}
		}
	}
}
