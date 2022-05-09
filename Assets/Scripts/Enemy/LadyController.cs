using Assets.Scripts.States;
using Assets.Scripts.States.EnemyStates.LadyStates;
using System;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class LadyController : NPCControllerBase<LadyStateMachine, LadyBaseState, LadyController>
    {
        public override Func<LadyStateMachine> ConstructStatemachine => () => new LadyStateMachine(this);

        [HideInInspector]
        public Animator Animator;

        [SerializeField]
        private LadyScriptableStates LadyScript;

        protected override void Awake()
        {
            base.Awake();
            Animator = GetComponentInChildren<Animator>();

            Animator.runtimeAnimatorController = LadyScript.GetRandomController();
        }

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            var isPlayer = collision.CompareTag("Player");
            if (isPlayer)
            {
                stateMachine.TransitionState(stateMachine.fleeingState);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var isPlayer = collision.CompareTag("Player");
            if (isPlayer)
            {
                stateMachine.PoplastState();
            }
        }
    }
}
