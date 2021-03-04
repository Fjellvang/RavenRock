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
			//TODO: add randomness ??
			var x = UnityEngine.Random.Range(-2, 2);
			//var x = Mathf.Round(Mathf.PerlinNoise(controller.projectileTarget.position.x, controller.projectileTarget.position.y) * 4 - 2);
			Debug.Log(x);
			var target = new Vector2(controller.projectileTarget.position.x + x, controller.projectileTarget.position.y);

			var bossTransform = controller.transform;
			//Calculate target velocity
			var targetVelocity = controller.physicsPredictor.CalculateVelocity(bossTransform.position, target, throwDirectionAngle);
			//determine facing
			var facing = controller.projectileTarget.position.x < controller.projectileSpawnPoint.position.x ? -1 : 1;
			var axe = UnityEngine.Object.Instantiate(controller.projectile,bossTransform.position, bossTransform.rotation, bossTransform);

			var rig = axe.GetComponent<Rigidbody2D>();
			rig.velocity = targetVelocity;
			rig.angularVelocity = facing * -150f;
			axe.transform.localScale = new Vector3(facing, 1);
		}
	}
}
