using Common.AnimationBasedFsm;
using Common.Animations;
using Common.Fsm;

namespace RogueDungeon.Enemies.States
{
    public abstract class EnemyState : BoundToAnimationState, ITypeBasedTransitionableState
    {
        private readonly EnemyStateConfig _config;

        protected sealed override AnimationData Animation => new(_config.Animation.name, _config.Duration);

        protected EnemyState(EnemyStateConfig config, IAnimator animator) : base(animator) => 
            _config = config;

        public abstract void CheckTransitions(ITypeBasedStateChanger stateChanger);
    }
}