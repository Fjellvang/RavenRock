using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player_States
{
	public class PlayerCarryingState : PlayerBaseState
	{
		public override void OnEnterState(PlayerController controller)
		{
			controller.KissSound.Play();
			controller.Animator.SetBool("isCarrying", true);
			controller.Dame.DestroyThis();
		}

		public override void Update(PlayerController controller)
		{
			base.Update(controller);
			if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.E))
			{
				//TODO: REFACOR
				controller.TransitionState(controller.idleState);
			}
		}

		public override void OnExitState(PlayerController controller)
		{
			controller.Animator.SetBool("isCarrying", false);
			controller.Dame.SpawnNew(controller.transform);
		}
	}
}
