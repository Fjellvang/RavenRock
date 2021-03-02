using Assets.Scripts.States.PlayerStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.States
{
	public class PlayerStateMachine : StateMachine<PlayerBaseState, PlayerController>
	{
		public PlayerStateMachine(PlayerController controller) : base(controller)
		{
			currentState = PlayerBaseState.idleState;
		}
	}
}
