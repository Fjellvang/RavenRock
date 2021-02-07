using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorMethods : MonoBehaviour
{
	private PlayerController playerController;
	private void Awake()
	{
		playerController = GetComponentInParent<PlayerController>();
	}

	public void Attack()
	{
		playerController.Attack();
	}
}
