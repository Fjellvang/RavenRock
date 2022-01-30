namespace Assets.Scripts.GameInput
{
    public class InputState
    {
        public float HorizontalMovement { get; set; }
        public bool IsPressingJump { get; set; }
        public bool IsPressingAttack { get; set; }
        public bool IsPressingBlock { get; set; }
        public bool IsPressingPause { get; set; }
    }
}
