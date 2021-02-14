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
		private bool jump = false;
		public bool JumpPressed { 
			get => jump; 
			set => jump = value;
		}
		public bool AttackPressed { get => attack; set => attack = value; }
		public bool BlockPressed { get => block; set => block = value; }
	}
}
