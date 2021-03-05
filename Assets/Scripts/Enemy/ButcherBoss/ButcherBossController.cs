using Assets.Scripts.CombatSystem;
using Assets.Scripts.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy.ButcherBoss
{
	public class ButcherBossController : MonoBehaviour, IAttacker, IAttackable
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
		public Attack weapon;
		[HideInInspector]
		public Animator animator;
		[HideInInspector]
		public GameObject player;

		public Health meatShieldHealth;

		private void Awake()
		{
			player = GameObject.FindWithTag("Player");
			stateMachine = new ButcherBossStateMachine(this);
			weapon = GetComponent<Attack>();
			animator = GetComponentInChildren<Animator>();
		}

		private void Update()
		{
			stateMachine.currentState.Update(this);
		}

		IAttackEffect[] attackEffects = new IAttackEffect[]
		{
			new ButcherBossAttack()
		};

		public void Attack()
		{
			weapon.DoAttack(attackEffects);
		}

		private void FixedUpdate()
		{
			stateMachine.currentState.FixedUpdate(this);
		}

		public void PowerFullAttack()
		{
			throw new NotImplementedException();
		}

		public void OnTakeDamage(GameObject attacker, IAttackEffect[] attackEffects)
		{
			meatShieldHealth.TakeDamage(Vector3.zero);
		}
	}
}
