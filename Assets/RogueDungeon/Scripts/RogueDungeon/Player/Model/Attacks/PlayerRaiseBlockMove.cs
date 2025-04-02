using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;
using RogueDungeon.Player.Model.Behaviours.Hands;

namespace RogueDungeon.Player.Model.Attacks
{
    public class PlayerRaiseBlockMove : PlayerMove
    {
        private readonly IItem _item;
        private readonly PlayerHandsBehaviour _hands;

        protected override InputKey RequiredKey => InputKey.Block;

        protected PlayerRaiseBlockMove(PlayerMoveConfig config, IAnimation animation, IPlayerInput playerInput, PlayerHandsBehaviour hands, IItem item) : base(config, animation, playerInput)
        {
            _hands = hands;
            _item = item;
        }

        protected override bool CanTransitionTo() =>
            base.CanTransitionTo() && _hands.IsDedicatedBlockingItem(_item);
    }
}