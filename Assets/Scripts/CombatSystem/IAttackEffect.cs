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


	public interface ITest<T>
	{
		T Component { get; }
	}
}
