using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours.Hands;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemRaiseBlockMove : ItemMove
    {
        private readonly IItem _item;
        private readonly PlayerHandsBehaviour _hands;
        private readonly IPlayerInput _input;
        private readonly InputUnit _blockKey;

        protected override float Duration => ((BlockingItemConfig)_item.Config).RaiseBlockDuration;

        protected ItemRaiseBlockMove(IAnimation animation, PlayerHandsBehaviour hands,
            IItem item, string id, IPlayerInput input) : base(id, animation, hands, input)
        {
            _hands = hands;
            _item = item;
            _input = input;
            _blockKey = _input.GetKey(InputKey.Block);
        }

        public override void Enter()
        {
            _blockKey.Reset();
            var itemKey = _input.GetKey(_hands.UseItemInput(_item));
            if(_item is Shield && itemKey.IsDown)
                itemKey.Reset();
            base.Enter();
        }

        protected override bool CanTransitionTo()
        {
            var itemKey = _input.GetKey(_hands.UseItemInput(_item));
            var isShieldWithUseItemInput = _item is Shield && (itemKey.IsDown || itemKey.IsHeld);
            var isDedicatedBlockerWithBlockInput = _hands.IsDedicatedToBlock(_item) && (_blockKey.IsDown || _blockKey.IsHeld);
            return  base.CanTransitionTo() && (isShieldWithUseItemInput || isDedicatedBlockerWithBlockInput);
        }
    }
}