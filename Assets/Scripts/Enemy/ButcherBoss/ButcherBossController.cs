using Assets.Scripts.Algorithms;
using Assets.Scripts.CombatSystem;
using Assets.Scripts.States;
using Assets.Scripts.States.EnemyStates.ButcherBossStates;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

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

		[HideInInspector]
		public ButcherBossPigPickup nextShield;

		private PigPickupManager pigPickupManager;

		[Inject]
		public void Construct(PigPickupManager pigPickupManager)
        {
			this.pigPickupManager = pigPickupManager;
        }

		protected override void Awake()
		{
			base.Awake();
			weapon = GetComponent<Attack>();
			animator = GetComponentInChildren<Animator>();
			spriteRenderer = GetComponentInChildren<SpriteRenderer>();

			meatShieldHealth.OnDeath += EvaluateShield;
		}

		private void EvaluateShield()
		{
			meatShieldHealth.gameObject.SetActive(false);
			var best = (999999f, default(Node<ButcherBossPigPickup>));
			var next = pigPickupManager.quadTree
				.FindNearestMarkUsed(new Algorithms.Point(this.transform.position), ref best, pigPickupManager.quadTree);
			nextShield = next?.Data;
			if (nextShield != null)
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
