using UnityEngine;

namespace Assets.Scripts.States.PlayerStates
{
    public class PlayerExhaustedState : PlayerBaseState
    {
        private Color originalColor;
        private float timeExhausted = 0;
        public override void OnEnterState(PlayerController controller)
        {
			controller.Animator.Play("Idle");
            controller.staminaMultiplier = controller.staminaBaseMultiplier * 2;
            originalColor = controller.playerRenderer.color;
            controller.playerRenderer.color = Color.yellow;
            timeExhausted = 2.5f; // 5 second exhaust for now... Make this cooler
        }

        public override void Update(PlayerController controller)
        {
            base.Update(controller);
            inputAxis *= 0.5f; //Poor mans exhaust.
            timeExhausted -= Time.deltaTime;
            if (timeExhausted <= 0)
            {
                controller.StateMachine.TransitionState(idleState);
            }
        }
        public override void OnExitState(PlayerController controller)
        {
            controller.playerRenderer.color = originalColor;
        }
    }
}
