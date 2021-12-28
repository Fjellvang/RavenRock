using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: rename to something apropriate
public class PlayerAnimatorMethods : MonoBehaviour
{
	public IAttacker attacker;
	public GameObject OnDeathSpawn;
	private void Awake()
	{
		attacker = GetComponentInParent<IAttacker>();
	}
	public enum AttackEnum { Regular, Heavy };
	public void Attack(AttackEnum attackType)
	{
		switch (attackType)
		{
			case AttackEnum.Heavy:
				attacker.PowerFullAttack();
				break;
			case AttackEnum.Regular:
				attacker.Attack();
				break;
			default:
				Debug.LogError("Undefined attack");
				break;
		}
	}
	public void Destroy()
	{
        //Used in Farmer. REFACTOR
        if (OnDeathSpawn != null)
        {
			Instantiate(OnDeathSpawn,transform.position, transform.rotation);
        }
		Destroy(gameObject);
	}
}


public interface IAttacker
{
	void Attack();
	void PowerFullAttack();
}
