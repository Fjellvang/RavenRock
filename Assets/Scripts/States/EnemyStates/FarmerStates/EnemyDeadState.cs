using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.EnemyStates.FarmerStates
{
	public class EnemyDeadState : EnemyBaseState
	{
		public override void OnEnterState(AI controller)
		{
			controller.GetComponent<CapsuleCollider2D>().enabled = false;
			controller.GetComponent<Rigidbody2D>().simulated = false;
			controller.Animator.SetTrigger("Death");
		}
	}
}
