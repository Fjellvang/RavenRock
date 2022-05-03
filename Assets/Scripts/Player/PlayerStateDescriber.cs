using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Player
{
    public class PlayerStateDescriber
    {
        public bool IsRunning { get; set; }
        public bool IsGrounded { get; set; }
        public bool IsExhausted { get; set; }
    }
}
