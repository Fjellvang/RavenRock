﻿using UnityEngine;

namespace Assets.Scripts.States.EnemyStates.FarmerStates
{
    public class EnemyFleeingState : EnemyBaseState
    {
        public float fleeDuration = 4f; //TODO: Move me
        float timefleeing = 0f;
        public override void OnEnterState(FarmerController controller)
        {
            timefleeing = fleeDuration;
            controller.Animator.Play("Running");
        }
        public override void Update(FarmerController controller)
        {
            var vectorTowardsPlayer = controller.player.transform.position - controller.transform.position;
            var direction = vectorTowardsPlayer.x < 0 ? 1 : -1;
            directionalForce = direction * controller.moveSpeed;
            timefleeing -= Time.deltaTime;

            if (timefleeing <= 0)
            {
                controller.stateMachine.TransitionState(controller.stateMachine.movingState);
            }
        }
    }
}