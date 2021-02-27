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
		Color defaultColor;
		float timer = 0;
		public override void OnEnterState(AI controller)
		{
			defaultColor = controller.SpriteRenderer.color;
			controller.SpriteRenderer.color = Color.yellow;
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

		public override void OnExitState(AI controller)
		{
			controller.SpriteRenderer.color = defaultColor;
		}
	}
}
