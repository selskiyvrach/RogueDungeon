using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemLowerBlockMove : PlayerMove
    {
        private readonly IItem _item;
        private readonly IPlayerInput _playerInput;

        protected override float Duration => _item.Config.LowerBlockDuration;

        protected ItemLowerBlockMove(IItem item, IAnimation animation, IPlayerInput playerInput, string id) : base(id, animation)
        {
            _item = item;
            _playerInput = playerInput;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && !_playerInput.HasInput(InputKey.Block);
    }
}