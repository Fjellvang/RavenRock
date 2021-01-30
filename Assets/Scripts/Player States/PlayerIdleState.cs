using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player_States
{
	public class PlayerIdleState : PlayerBaseState
	{

		public override void OnEnterState(PlayerController controller)
		{
		}

		public override void OnExitState(PlayerController controller)
		{
		}

		public override void Update(PlayerController controller)
		{
			base.Update(controller);
			if(Mathf.Abs(controller.CharacterController.Velocity.x) >= (0.01f))
			{
				controller.TransitionState(movingState);
			}
		}
	}
}
