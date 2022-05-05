using Assets.Scripts.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.EnemyStates.LadyStates
{
    public class LadyFleeingState : LadyBaseState
    {
        public override void OnEnterState(LadyController controller)
        {
            controller.Animator.SetBool("IsMoving", true);
        }

        public override void OnExitState(LadyController controller)
        {
            controller.Animator.SetBool("IsMoving", false);
        }

        public override void Update(LadyController controller)
        {
            var vectorTowardsPlayer = controller.player.transform.position - controller.transform.position;
            var direction = vectorTowardsPlayer.x < 0 ? 1 : -1;
            directionalForce = direction * controller.moveSpeed;
        }
        
		public override void FixedUpdate(LadyController controller)
		{
			controller.controller.Move(directionalForce * Time.fixedDeltaTime, false, false);
		}
    }
}
