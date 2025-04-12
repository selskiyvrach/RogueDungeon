using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public abstract class HandsState : Move
    {
        private readonly HandHeldMoveConfig _config;
        protected override float Duration => _config.Duration;

        protected HandsState(HandHeldMoveConfig config, IAnimation animation) : base(config, animation) => 
            _config = config;
    }
}