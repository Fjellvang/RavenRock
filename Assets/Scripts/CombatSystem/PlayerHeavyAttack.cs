using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CombatSystem
{
	public class PlayerHeavyAttack : IAttackEffect
	{
		public void OnFailedAttack(GameObject attacker, GameObject attacked)
		{
		}

		public void OnSuccessFullAttack(GameObject attacker, GameObject attacked)
		{
			//TODO: REFACTOR This works SHIT with the boss...
            var attackDelta = attacked.transform.position - attacker.transform.position; //could cache transform for micro optimization
            attacked.GetComponent<Health>().TakeCriticalDamage(attackDelta);
            var ai= attacked.GetComponent<AI>();
            ai.StateMachine.TransitionState(ai.StateMachine.stunnedState);
		}
	}
}
