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
