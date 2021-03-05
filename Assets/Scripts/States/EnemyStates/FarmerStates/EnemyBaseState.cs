using Assets.Scripts.Player_States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.EnemyStates.FarmerStates
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
		public override void FixedUpdate(AI controller)
		{
			controller.controller.Move(directionalForce * Time.fixedDeltaTime, false, false);
		}

		protected static bool IsPlayerInAttackRange(AI controller)
		{
			var weaponToPlayer = controller.weapon.axeAttack.position - controller.player.transform.position;
			var withinRange = Mathf.Abs(weaponToPlayer.x) <= controller.weapon.attackRadius;
			return withinRange;
		}
	}
}
