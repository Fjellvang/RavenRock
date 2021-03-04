using Assets.Scripts.Enemy.ButcherBoss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.EnemyStates.ButcherBossStates
{
	public class ButcherBossThrowingState : ButcherBossBaseState
	{
		private ButcherBossController controller;

		static readonly Vector3 throwDirection = new Vector3(1, 1, 0).normalized;
		static readonly float throwDirectionAngle = Mathf.Atan2(throwDirection.y, throwDirection.x);
		private float timeToThrow = 1;
		private float timer = 0;

		public override void OnEnterState(ButcherBossController controller)
		{
			this.controller = controller;//TODO: this is kind of dirty, but shouldnt be an issue for now.
			timer = 0;
		}

		public override void OnExitState(ButcherBossController controller)
		{
		}

		public override void Update(ButcherBossController controller)
		{
			var targetDelta = Mathf.Abs(controller.transform.position.x - controller.projectileTarget.position.x);

			timer += Time.deltaTime;
			if (timer > timeToThrow)
			{
				ThrowAxe();
				timer = 0;
			}

			if (targetDelta < controller.projectileRange)
			{
				//switch state
				controller.stateMachine.TransitionState(controller.stateMachine.idleState);
			}
		}


		void ThrowAxe()
		{
			//Add alittle randomness to the angle
			var x = Mathf.PerlinNoise(0, Time.time) * 2f - 1f;

			//Calculate target velocity
			var targetVelocity = controller.physicsPredictor.CalculateVelocity(controller.projectileSpawnPoint.position, controller.projectileTarget.position, throwDirectionAngle + x);
			//determine facing
			var facing = controller.projectileTarget.position.x < controller.projectileSpawnPoint.position.x ? -1 : 1;
			var axe = UnityEngine.Object.Instantiate(controller.projectile, controller.projectileSpawnPoint.position, Quaternion.identity);

			var rig = axe.GetComponent<Rigidbody2D>();
			rig.velocity = targetVelocity;
			axe.transform.localScale = new Vector3(facing, 1);
		}
	}
}
