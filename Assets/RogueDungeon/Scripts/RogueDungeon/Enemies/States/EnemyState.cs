using Common.AnimationBasedFsm;
using Common.Animations;

namespace RogueDungeon.Enemies.States
{
    public abstract class EnemyState : BoundToAnimationState
    {
        public EnemyStateConfig Config { get; }
        public Priority Priority => Config.Priority;
        protected override IAnimation Animation { get; }
        protected override float Duration => Config.Duration;

        protected EnemyState(EnemyStateConfig config, IAnimation animation)
        {
            Config = config;
            Animation = animation;
        }

        protected override void OnAnimationEvent(string name)
        {
        }
    }
}