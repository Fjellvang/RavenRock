using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CombatSystem
{
	public class ButcherCleaverAttack : IAttackEffect
	{
		public void OnFailedAttack(GameObject attacker, GameObject attacked)
		{
		}

		public void OnSuccessFullAttack(GameObject attacker, GameObject attacked)
		{
			Debug.LogFormat("Attacked {0}", attacked);
		}
	}
}
