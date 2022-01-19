using Assets.Scripts.CombatSystem;
using Assets.Scripts.Player_States;
using Assets.Scripts.States;
using System;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(CharacterController2D))]
    public abstract class NPCControllerBase<TStateMachine,TBaseState, TController> 
        : MonoBehaviour, IAttackable
        where TController : NPCControllerBase<TStateMachine, TBaseState, TController>
        where TStateMachine : StateMachine<TBaseState, TController>
        where TBaseState : BaseState<TController>
    {
        public float moveSpeed = 10f;

        [HideInInspector]
        public CharacterController2D controller;

        public TStateMachine stateMachine;

		[HideInInspector]
		public GameObject player;

        public abstract Func<TStateMachine> ConstructStatemachine { get; } 
        protected virtual void Start()
        {

        }

        protected virtual void Awake()
        {
			player = GameObject.FindWithTag("Player");
            stateMachine = ConstructStatemachine();
            controller = GetComponent<CharacterController2D>();
        }

        protected virtual void Update()
        {
            stateMachine.currentState.Update((TController)this);
        }

        protected virtual void FixedUpdate()
        {
            stateMachine.currentState.FixedUpdate((TController)this);
        }

        public virtual void OnTakeDamage(GameObject attacker, IAttackEffect[] attackEffects)
        {
            //TODO: Famer and butcher boss handles this differntly - investigate!
            stateMachine.currentState.OnTakeDamage((TController)this, attacker, attackEffects);
        }
    }
}
