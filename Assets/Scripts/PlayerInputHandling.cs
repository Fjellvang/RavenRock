using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{

	public interface InputHandler
	{
		bool JumpPressed();
		bool AttackPressed();
		bool BlockPressed();
	}
	public class PlayerInputDebugHandler : InputHandler
	{
		public bool AttackPressed()
		{
			return Input.GetButtonDown("Attack");
		}

		public bool BlockPressed()
		{
			return Input.GetButton("Block");
		}

		public bool JumpPressed()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Space);
		}
	}
}
