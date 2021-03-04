using Assets.Scripts.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy.ButcherBoss
{
	public class ButcherBossController : MonoBehaviour
	{
		[Header("Projectile Setting")]
		public GameObject projectile; //This axe that the boss throws
		public Transform projectileSpawnPoint;
		public Transform projectileTarget;
		public float projectileRange = 2f;

		[Header("Misc")]
		public float movementSpeed = 20;
		public CharacterController2D controller;
		public ButcherBossStateMachine stateMachine;
		public PhysicsPredictor physicsPredictor = new PhysicsPredictor();
		[HideInInspector]
		public Attack Attack;

		private void Awake()
		{
			stateMachine = new ButcherBossStateMachine(this);
			Attack = GetComponent<Attack>();
		}

		private void Update()
		{
			stateMachine.currentState.Update(this);
		}

		private void FixedUpdate()
		{
			stateMachine.currentState.FixedUpdate(this);
		}
	}
}
