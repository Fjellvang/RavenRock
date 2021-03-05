using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CombatSystem
{
	public class PlayerRegularAttack : IAttackEffect
	{
		public void OnFailedAttack(GameObject attacker, GameObject attacked)
		{
		}

		public void OnSuccessFullAttack(GameObject attacker, GameObject attacked)
		{
			//Same TODO as Heavy attack... Maybe introduce attacked effect...
            //var attackDelta = attacked.transform.position - attacker.transform.position; //could cache transform for micro optimization
            attacked.GetComponent<Health>().TakeDamage(Vector3.zero);
		}
	}
}
