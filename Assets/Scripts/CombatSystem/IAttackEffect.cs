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
		void OnSuccessFullAttack(GameObject attacked);

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

		public void OnSuccessFullAttack(GameObject attacked)
		{
			attacked.GetComponent<PlayerHealth>().TakeDmg();
		}
	}
}
