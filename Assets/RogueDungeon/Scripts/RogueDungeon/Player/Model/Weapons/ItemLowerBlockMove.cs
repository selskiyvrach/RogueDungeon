using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;
using RogueDungeon.Player.Model.Behaviours.Hands;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemLowerBlockMove : PlayerMove
    {
        private readonly IPlayerInput _input;
        private readonly PlayerHandsBehaviour _hands;
        private readonly IItem _item;

        protected override float Duration => _item.Config.LowerBlockDuration;

        protected ItemLowerBlockMove(IItem item, IAnimation animation, string id, PlayerHandsBehaviour hands, IPlayerInput input) : base(id, animation)
        {
            _item = item;
            _hands = hands;
            _input = input;
        }

        protected override bool CanTransitionTo()
        {
            if (_hands.IsDedicatedToBlock(_item) && !_input.IsHeld(InputKey.Block))
                return false;
            
            if(_item is Shield && !_input.IsHeld(_hands.UseItemInput(_item)))
                return false;
            
            return base.CanTransitionTo();
        }
    }
}