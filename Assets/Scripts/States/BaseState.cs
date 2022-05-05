using Assets.Scripts.CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player_States
{
	public abstract class BaseState<T>
	{
		public abstract void OnEnterState(T controller);
		public abstract void Update(T controller);
		public virtual void FixedUpdate(T controller) { }

		public abstract void OnExitState(T controller);
		public virtual void OnTakeDamage(T controller, GameObject attacker, IAttackEffect[] attackEffects) {}
	}
}
