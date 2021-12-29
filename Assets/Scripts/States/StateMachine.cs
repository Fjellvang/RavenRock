using Assets.Scripts.Player_States;
using System.Collections.Generic;

namespace Assets.Scripts.States
{
    public abstract class StateMachine<TBaseState, TController> 
		where TBaseState : BaseState<TController>
	{
		public TBaseState currentState;
		private TController controller;

		public readonly Stack<TBaseState> stateStack = new Stack<TBaseState>();

		public StateMachine(TController controller)
		{
			this.controller = controller;
		}

		public void PoplastState()
		{
			currentState.OnExitState(controller);
			currentState = stateStack.Pop();//TODO: nullcheck ?
			currentState.OnEnterState(controller);
		}

		public void TransitionState(TBaseState newState)
		{
			currentState.OnExitState(controller);
			stateStack.Push(currentState);
			currentState = newState;
			currentState.OnEnterState(controller);
		}
	}
}
