using Assets.Scripts.Player_States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.States
{
	public abstract class StateMachine<T, TController> where T : BaseState<TController>
	{
		public T currentState;
		private TController controller;

		public readonly Stack<T> stateStack = new Stack<T>();

		public StateMachine(TController controller)
		{
			this.controller = controller;
		}

		public void PoplastState()//TODO: Get better naming.
		{
			currentState.OnExitState(controller);
			currentState = stateStack.Pop();//TODO: nullcheck ?
			currentState.OnEnterState(controller);
		}

		public void TransitionState(T newState)
		{
			currentState.OnExitState(controller);
			stateStack.Push(currentState);
			currentState = newState;
			currentState.OnEnterState(controller);
		}
	}
}
