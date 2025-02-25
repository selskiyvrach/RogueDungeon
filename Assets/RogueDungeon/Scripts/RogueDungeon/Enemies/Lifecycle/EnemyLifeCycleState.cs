using Common.AnimationBasedFsm;
using Common.Animations;
using Common.Fsm;

namespace RogueDungeon.Enemies
{
    public abstract class EnemyLifeCycleState : BoundToAnimationState, ITypeBasedTransitionableState
    {
        private readonly EnemyLifeCycleConfig _config;
        protected EnemyLifeCycleState(EnemyLifeCycleConfig config, IAnimator animator) : base(animator) => 
            _config = config;

        public abstract void CheckTransitions(ITypeBasedStateChanger stateChanger);
    }
}