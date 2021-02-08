using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: rename to something apropriate
public class PlayerAnimatorMethods : MonoBehaviour
{
	public EntityController controller;
	private IAttack attack;
	private void Awake()
	{
		controller = GetComponentInParent<EntityController>();
		attack = controller as IAttack;
	}

	public void Attack()
	{
		attack.Attack();
	}
}


interface IAttack
{
	void Attack();
}
