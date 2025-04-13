using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;
using RogueDungeon.Player.Model.Behaviours.Hands;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemTransitionBetweenAttacksMove : PlayerInputMove
    {
        private readonly PlayerHandsBehaviour _hands;
        private readonly IWeapon _item;
        protected override float Duration => ((WeaponConfig)_item.Config).TransitionBetweenAttacksDuration;

        protected override InputKey RequiredKey => _hands.ThisHand(_item) == _hands.RightHand 
            ? InputKey.UseRightHandItem 
            : InputKey.UseLeftHandItem;

        public ItemTransitionBetweenAttacksMove(IWeapon item, IAnimation animation, IPlayerInput playerInput, string id, PlayerHandsBehaviour hands) : base(id, animation, playerInput)
        {
            _item = item;
            _hands = hands;
        }
    }
}