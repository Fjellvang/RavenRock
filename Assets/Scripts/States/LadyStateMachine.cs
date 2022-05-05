using Assets.Scripts.Enemy;
using Assets.Scripts.States.EnemyStates.LadyStates;

namespace Assets.Scripts.States
{
    public class LadyStateMachine : StateMachine<LadyBaseState, LadyController>
    {
        public LadyStateMachine(LadyController controller) : base(controller)
        {
            currentState = idleState;
        }

        public readonly LadyIdleState idleState = new LadyIdleState();
        public readonly LadyFleeingState fleeingState = new LadyFleeingState();
    }
}
