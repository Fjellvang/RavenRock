using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CombatSystem
{
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
			attacked.GetComponent<Health>().TakeDamage(attacker,1);
		}
	}
}
