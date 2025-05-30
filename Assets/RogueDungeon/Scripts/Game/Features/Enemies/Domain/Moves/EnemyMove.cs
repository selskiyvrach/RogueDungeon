using Libs.Animations;
using Libs.Movesets;

namespace Game.Features.Enemies.Domain.Moves
{
    public abstract class EnemyMove : Move
    {
        public abstract Priority Priority { get; }
        protected override IAnimation Animation { get; }

        protected EnemyMove(IAnimation animation, string id) : base(id, animation) => 
            Animation = animation;

        protected override void OnAnimationEvent(string name)
        {
        }
    }
}