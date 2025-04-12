using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemIdleMove : PlayerMove
    {
        private readonly IItem _item;
        protected override float Duration => _item.Config.IdleAnimationDuration;

        public ItemIdleMove(IItem item, PlayerMoveConfig config, IAnimation animation, IPlayerInput playerInput) : base(config, animation, playerInput) => 
            _item = item;
    }
}