using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Android_UI
{
	public class AndroidButtonHandler : IInputHandler
	{
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
		public bool AttackPressed()
		{
			throw new NotImplementedException();
		}

		public bool BlockPressed()
		{
			throw new NotImplementedException();
		}


		public bool JumpPressed()
		{
			throw new NotImplementedException();
		}
	}
}
