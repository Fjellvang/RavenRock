using Assets.Scripts.Player_States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.EnemyStates
{
	public class AttackingState : EnemyBaseState
	{
		public override void OnEnterState(AI controller)
		{
            controller.Animator.SetBool("inRange", true);
		}

		public override void OnExitState(AI controller)
		{
            controller.Animator.SetBool("inRange", false);
		}

		public override void Update(AI controller)
		{
			//TODO: Fix this. maybe add animation event or something better?
			var inAttack = controller.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < .9f;
			if (!controller.weapon.withinRange && !inAttack)
			{
				controller.TransitionState(controller.movingState);
			}
		}
	}
}
