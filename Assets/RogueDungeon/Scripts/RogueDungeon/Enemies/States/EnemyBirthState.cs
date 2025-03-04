using Common.Animations;
using Common.Fsm;

namespace RogueDungeon.Enemies.States
{
    public class EnemyBirthState : EnemyState
    {
        public EnemyBirthState(EnemyStateConfig config, IAnimator animator) : base(config, animator) {}

        public override void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            if(IsFinished)
                stateChanger.ChangeState<EnemyIdleState>();
        }
    }
}