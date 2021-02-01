using Assets.Scripts.Player_States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.PlayerStates
{
	/// <summary>
	/// Handles movement.
	/// </summary>
	public abstract class PlayerLocomotiveBaseState : PlayerBaseState
	{
		public override void FixedUpdate(PlayerController controller)
		{
			controller.CharacterController.Move(inputAxis * controller.acceleration * Time.fixedDeltaTime, false, jump);
			jump = false;
		}
	}
}
