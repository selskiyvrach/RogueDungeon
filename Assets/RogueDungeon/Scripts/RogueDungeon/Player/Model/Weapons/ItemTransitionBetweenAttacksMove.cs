using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemTransitionBetweenAttacksMove : PlayerMove
    {
        private readonly IWeapon _item;
        protected override float Duration => ((WeaponConfig)_item.Config).TransitionBetweenAttacksDuration;
        
        public ItemTransitionBetweenAttacksMove(IWeapon item, PlayerMoveConfig config, IAnimation animation, IPlayerInput playerInput) : base(config, animation, playerInput) => 
            _item = item;
    }
}