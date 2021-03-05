using Assets.Scripts.Enemy.ButcherBoss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.EnemyStates.ButcherBossStates
{
	public class ButcherBossEnrageState : ButcherBossShieldDownState
	{
		public override void OnEnterState(ButcherBossController controller)
		{
			controller.spriteRenderer.color = Color.red;
		}

		public override void OnExitState(ButcherBossController controller)
		{
		}

		public override void Update(ButcherBossController controller)
		{
		}
	}
}
