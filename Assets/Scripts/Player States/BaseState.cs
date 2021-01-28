using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player_States
{
	public abstract class BaseState<T>
	{
		public abstract void OnEnterState(T controller);
		public abstract void Update(T controller);

		public abstract void OnExitState(T controller);
	}
}
