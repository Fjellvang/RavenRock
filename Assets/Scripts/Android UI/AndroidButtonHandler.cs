using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Android_UI
{
	public class AndroidButtonHandler : IInputHandler
	{
		private AndroidButtonHandler()
		{

		}
		private static AndroidButtonHandler statickInstance;
		public static AndroidButtonHandler Instance
		{
			get{
				if (statickInstance is null)
				{
					statickInstance = new AndroidButtonHandler();
				}
				return statickInstance;
			}
		}

		private bool attack = false;
		private bool block = false;
		private bool jumpButtonDown = false;
		public bool JumpButton { 
			get
			{
				return jumpButtonDown;
			}
			set {
				var isJumpDown = value;
				if (isJumpDown)
				{
					returnedJumpButton = false;
				}
				else
				{
					returnedJumpButtonUp = false;
				}
				jumpButtonDown = value; 
			}
		}
		public bool AttackPressed { get => attack; set => attack = value; }
		public bool BlockPressed { get => block; set => block = value; }

		bool returnedJumpButtonUp = false;
		bool returnedJumpButton = false;
		public bool JumpButtonDown { get
			{
				var retval = jumpButtonDown;
				if (!returnedJumpButton && retval)
				{
					returnedJumpButton = true;
					return true;
				}
				return false;
			} 
		}
	}
}
