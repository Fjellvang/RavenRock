using Assets.Scripts.Enemy;
using Assets.Scripts.Player_States;

namespace Assets.Scripts.States.EnemyStates.LadyStates
{
    public abstract class LadyBaseState : BaseState<LadyController>
    {
        protected float directionalForce = 0;

    }
}
