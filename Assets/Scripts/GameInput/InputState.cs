namespace Assets.Scripts.GameInput
{
    public class InputState 
    {
        public float HorizontalMovement { get; set; }
        public bool PressedJump { get; set; }
        public bool IsHoldingJump { get; set; }
        public bool IsPressingAttack { get; set; }
        public bool IsPressingBlock { get; set; }
        public bool IsPressingPause { get; set; }
    }
}
