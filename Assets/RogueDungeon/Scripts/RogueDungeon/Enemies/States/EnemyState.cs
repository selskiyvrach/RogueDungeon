using Common.AnimationBasedFsm;
using Common.Animations;

namespace RogueDungeon.Enemies.States
{
    public class EnemyState : BoundToAnimationState
    {
        public EnemyStateConfig Config { get; }
        public Priority Priority => Config.Priority;
        protected override IAnimation Animation { get; }

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