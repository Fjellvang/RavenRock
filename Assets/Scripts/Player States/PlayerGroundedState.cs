using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player_States
{
	public abstract class PlayerGroundedState : PlayerBaseState
	{
		public override void Update(PlayerController controller)
		{
			base.Update(controller);
		}
	}
}
