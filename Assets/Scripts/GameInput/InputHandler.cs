using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameInput
{
    public class InputHandler : ITickable
    {
        private readonly InputState inputState;

        public InputHandler(InputState inputState)
        {
            this.inputState = inputState;
        }
        public void Tick()
        {
            inputState.HorizontalMovement = Input.GetAxis("Horizontal");
            inputState.IsPressingAttack = Input.GetButton("Attack");
            inputState.PressedJump = Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Space);
            inputState.IsHoldingJump = Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.Space);
            inputState.IsPressingBlock = Input.GetButton("Block");
            inputState.IsPressingPause = Input.GetKeyDown(KeyCode.Escape);
        }
    }
}
