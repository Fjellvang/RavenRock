using Assets.Scripts.CombatSystem;
using Assets.Scripts.States;
using Assets.Scripts.States.EnemyStates.ButcherBossStates;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enemy.ButcherBoss
{
    [RequireComponent(typeof(CharacterController2D))]
	public class ButcherBossController : 
		NPCControllerBase<ButcherBossStateMachine, ButcherBossBaseState, ButcherBossController>,
		IAttacker
	{
		[Header("Projectile Setting")]
		public GameObject projectile; //This axe that the boss throws
		public Transform projectileSpawnPoint;
		public Transform projectileTarget;
		public float projectileRange = 2f;

		[Header("Misc")]
		public float movementSpeed = 20;
		public PhysicsPredictor physicsPredictor = new PhysicsPredictor();
		[HideInInspector]
		public Attack weapon;
		[HideInInspector]
		public Animator animator;
		[HideInInspector]
		public SpriteRenderer spriteRenderer;

		public Health meatShieldHealth;
		public Health bossHealth;


		public List<ButcherBossPigPickup> remainingShields = new List<ButcherBossPigPickup>();

		protected override void Awake()
		{
			base.Awake();
			weapon = GetComponent<Attack>();
			animator = GetComponentInChildren<Animator>();
			spriteRenderer = GetComponentInChildren<SpriteRenderer>();

			remainingShields = GameObject.FindGameObjectsWithTag("PigPickup")
				.Select(x => x.GetComponent<ButcherBossPigPickup>())
				.ToList();

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

		IAttackEffect[] attackEffects = new IAttackEffect[]
		{
			new ButcherBossAttack()
		};

		public override Func<ButcherBossStateMachine> ConstructStatemachine => () => new ButcherBossStateMachine(this);

        public void Attack()
		{
			weapon.DoAttack(attackEffects);
		}


		public void PowerFullAttack()
		{
			throw new NotImplementedException();
		}

	}
}
