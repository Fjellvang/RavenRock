using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: rename to something apropriate
public class PlayerAnimatorMethods : MonoBehaviour
{
	public IAttacker attacker;
	//private IAttack attack;
	private void Awake()
	{
		attacker = GetComponentInParent<IAttacker>();
		//attack = controller as IAttack;
	}

	public void Attack()
	{
		attacker.Attack();
	}

	public void CriticalAttack()
	{
		attacker.PowerFullAttack();
	}

	public void Destroy()
	{
		//Used in Farmer. REFACTOR
		Destroy(gameObject);
	}
}


public interface IAttacker
{
	void Attack();
	void PowerFullAttack();
}
