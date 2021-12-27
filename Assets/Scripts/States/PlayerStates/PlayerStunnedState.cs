using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.PlayerStates
{
	public class PlayerStunnedState : PlayerLocomotiveBaseState
	{
		private Color origColor;
		private float timer = 0;
		private float stunDuration = 1f;
		
		public override void OnEnterState(PlayerController controller)
		{
			origColor = controller.playerRenderer.color;
			controller.playerRenderer.color = Color.yellow;
			controller.Animator.Play("Idle"); //TODO: GET STUNNED if kept.
			timer = 0;
		}

		public override void Update(PlayerController controller)
		{
			timer += Time.deltaTime;
			if (timer > stunDuration)
			{
				controller.StateMachine.PoplastState();//TODO: Verify no funny business if last state was attack
			}
		}
		public override void OnExitState(PlayerController controller)
		{
			controller.playerRenderer.color = origColor;
		}
	}
}
