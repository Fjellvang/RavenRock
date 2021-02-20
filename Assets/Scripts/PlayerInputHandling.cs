using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{

	public interface IInputHandler
	{
		bool JumpButtonDown { get; }
		bool JumpButton { get; set; }
		bool AttackPressed { get; set; }
		bool BlockPressed { get; set; }
	}
	public class PlayerInputDebugHandler : IInputHandler
	{
		public bool JumpButton { get => Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Space)	; set { } }
		public bool AttackPressed { get => Input.GetButtonDown("Attack"); set { } }
		public bool BlockPressed { get => Input.GetButton("Block"); set { } }

		public bool JumpButtonDown { get => JumpButton; }
	}
}
