using Assets.Scripts.States.PlayerStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CombatSystem
{
	public interface IAttackEffect
	{
		void OnSuccessFullAttack(GameObject attacker, GameObject attacked);

		void OnFailedAttack(GameObject attacker,GameObject attacked);
	}

	public class FarmerAttack : IAttackEffect
	{
		public void OnFailedAttack(GameObject attacker, GameObject attacked)
		{
			//Todo: Template to avoid getcomponent ? will this even work ?
			var ai = attacker.GetComponent<AI>();
			ai.StateMachine.TransitionState(ai.StateMachine.stunnedState);
		}

		public void OnSuccessFullAttack(GameObject attacker, GameObject attacked)
		{
			attacked.GetComponent<PlayerHealth>().TakeDmg();
		}
	}

	public class ButcherBossAttack : IAttackEffect
	{
		public void OnFailedAttack(GameObject attacker, GameObject attacked)
		{
			var pc = attacked.GetComponent<PlayerController>();
			var delta = (attacked.transform.position - attacker.transform.position).normalized;
			delta.y += 1f;
			pc.CharacterController.m_Rigidbody2D.AddForce(delta * 10, ForceMode2D.Impulse);
			pc.health.TakeDmg();
		}

		public void OnSuccessFullAttack(GameObject attacker, GameObject attacked)
		{
			var pc = attacked.GetComponent<PlayerController>();
			var delta = (attacked.transform.position - attacker.transform.position).normalized;
			delta.y += 1f;
			pc.CharacterController.m_Rigidbody2D.AddForce(delta * 10, ForceMode2D.Impulse);
			pc.StateMachine.TransitionState(PlayerBaseState.stunnedState);
			pc.health.TakeDmg();
		}
	}
}
