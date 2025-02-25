using Common.Animations;
using Common.Fsm;

namespace RogueDungeon.Enemies
{
    public class EnemyBirthState : EnemyLifeCycleState
    {
        private readonly EnemyLifeCycleConfig _config;
        protected override AnimationData Animation => new(_config.BirthAnimation.name, _config.BirthDuration);

        public EnemyBirthState(EnemyLifeCycleConfig config, IAnimator animator) : base(config, animator) => 
            _config = config;

        public override void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            if(IsFinished)
                stateChanger.ChangeState<EnemyBeingAliveState>();
        }
    }
}