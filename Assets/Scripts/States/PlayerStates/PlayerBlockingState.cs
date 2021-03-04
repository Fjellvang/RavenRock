using Assets.Scripts.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.PlayerStates
{
	public class PlayerBlockingState : PlayerLocomotiveBaseState
	{
		public override void OnEnterState(PlayerController controller)
		{
			controller.Animator.Play("Blocking");
		}

		public override void Update(PlayerController controller)
		{
			inputAxis = 0;
			if (!Input.GetButton("Block"))
			{
				controller.StateMachine.PoplastState();
			}
		}

		public override void OnExitState(PlayerController controller)
		{
		}

		public override void OnTakeDamage(PlayerController controller, GameObject attacker, IAttackEffect[] attackEffects)
		{
			var delta = (controller.transform.position - attacker.transform.position).normalized;
			var facing = new Vector3(controller.transform.localScale.x, 0, 0); //we flip with local scale, so use just that.
			var dot = Vector3.Dot(delta, facing);
			var successfullAttack = dot > 0; //Positive dot product means we are facing the same way, IE player is attacked in the back.
			for (int i = 0; i < attackEffects.Length; i++)
			{
				var effect = attackEffects[i];
				if (successfullAttack)
				{
					effect.OnSuccessFullAttack(controller.gameObject);
				}
				else
				{
					effect.OnFailedAttack(attacker, controller.gameObject);
				}
			}
			//bool attackFromLeft = attackDirection.x > 0;
			//bool facingRight = controller.CharacterController.FacingRight;
			//if (attackFromLeft && facingRight || !attackFromLeft && !facingRight)
			//{
			//	//We are attacked in the back
			//	controller.health.TakeDmg();
			//	return true;
			//}
			//controller.health.Block();
			//return false;
		}
	}
}
