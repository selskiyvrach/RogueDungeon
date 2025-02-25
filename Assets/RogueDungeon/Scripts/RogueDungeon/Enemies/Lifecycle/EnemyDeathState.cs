using Common.Animations;
using Common.Fsm;

namespace RogueDungeon.Enemies
{
    public class EnemyDeathState : EnemyLifeCycleState
    {
        private readonly EnemyLifeCycleConfig _config;
        protected override AnimationData Animation => new(_config.DeathAnimation.name, _config.DeathDuration);

        public EnemyDeathState(EnemyLifeCycleConfig config, IAnimator animator) : base(config, animator) => 
            _config = config;

        public override void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            // no transitions from death...
        }
    }
}