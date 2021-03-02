using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.PlayerStates
{
	public class PlayerCarryingLadyState : PlayerLocomotiveBaseState
	{

		public override void Update(PlayerController controller)
		{
			base.Update(controller);
			if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.E))
			{
				//TODO: Could pop here, but for now idle should be fine
				controller.StateMachine.TransitionState(idleState);
			}
		}

		public override void OnExitState(PlayerController controller)
		{
			controller.Animator.SetBool("isCarrying", false);
			controller.Dame.SpawnNew(controller.transform);
		}

		public override void OnEnterState(PlayerController controller)
		{
			controller.KissSound.Play();
			controller.Animator.SetBool("isCarrying", true);
			controller.Dame.DestroyThis();
		}
	}
}
