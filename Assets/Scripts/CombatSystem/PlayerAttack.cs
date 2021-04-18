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
			var damage = UnityEngine.Random.Range(10, 20);
            attacked.GetComponent<Health>().TakeDamage(attacker, damage);
		}
	}
}
