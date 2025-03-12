using Common.AnimationBasedFsm;
using Common.Animations;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyState : BoundToAnimationState
    {
        private readonly EnemyStateConfig _config;
        public Priority Priority => _config.Priority;
        protected override IAnimation Animation { get; }

        protected EnemyState(EnemyStateConfig config, IAnimation animation)
        {
            _config = config;
            Animation = animation;
        }

        protected override void OnAnimationEvent(string name)
        {
        }
    }
}