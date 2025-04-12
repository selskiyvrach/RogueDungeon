using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class PlayerLowerBlockMove : PlayerMove
    {
        private readonly IItem _item;
        private readonly IPlayerInput _playerInput;

        protected override float Duration => _item.Config.LowerBlockDuration;

        protected PlayerLowerBlockMove(IItem item, PlayerMoveConfig config, IAnimation animation, IPlayerInput playerInput) : base(config, animation, playerInput)
        {
            _item = item;
            _playerInput = playerInput;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && !_playerInput.HasInput(InputKey.Block);
    }
}