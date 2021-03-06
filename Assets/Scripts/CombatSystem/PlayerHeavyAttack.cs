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
            attacked.GetComponent<Health>().TakeCriticalDamage(attacker);
		}
	}
}
