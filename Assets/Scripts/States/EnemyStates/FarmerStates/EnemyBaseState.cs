using Assets.Scripts.Player_States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.EnemyStates.FarmerStates
{
	public class EnemyBaseState : BaseState<FarmerController>
	{
		protected float directionalForce = 0;
		public override void OnEnterState(FarmerController controller)
		{
		}

		public override void OnExitState(FarmerController controller)
		{
		}

		public override void Update(FarmerController controller)
		{
		}
		public override void FixedUpdate(FarmerController controller)
		{
			controller.controller.Move(directionalForce * Time.fixedDeltaTime, false, false);
		}

	}
}
