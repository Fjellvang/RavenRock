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
		[HideInInspector]
		public SpriteRenderer spriteRenderer;

		public Health meatShieldHealth;
		public Health bossHealth;


		public List<ButcherBossPigPickup> remainingShields = new List<ButcherBossPigPickup>();

		private void Awake()
		{
			player = GameObject.FindWithTag("Player");
			stateMachine = new ButcherBossStateMachine(this);
			weapon = GetComponent<Attack>();
			animator = GetComponentInChildren<Animator>();
			spriteRenderer = GetComponentInChildren<SpriteRenderer>();

			remainingShields = GameObject.FindGameObjectsWithTag("PigPickup").Select(x => x.GetComponent<ButcherBossPigPickup>()).ToList();

			meatShieldHealth.OnDeath += EvaluateShield;
		}

		private void EvaluateShield()
		{
			meatShieldHealth.gameObject.SetActive(false);
			var shieldsRemaning = remainingShields.Count > 0;
			if (shieldsRemaning)
			{
				stateMachine.TransitionState(stateMachine.searchForShieldState);
				return;
			}

			stateMachine.TransitionState(stateMachine.enrageState);
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
			stateMachine.currentState.OnTakeDamage(this, attacker, attackEffects);
			//TODO: Use the attack effects.. currently needs refactor as player attack adds force. we dont want that on the boss.
		}
	}
}
