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

        protected override float Duration => ((BlockingItemConfig)_item.Config).RaiseBlockDuration;

        protected ItemRaiseBlockMove(IAnimation animation, PlayerHandsBehaviour hands,
            IItem item, string id, IPlayerInput input) : base(id, animation, hands, input)
        {
            _hands = hands;
            _item = item;
            _input = input;
        }

        public override void Enter()
        {
            if(_input.IsDown(InputKey.Block))
                _input.ConsumeInput(InputKey.Block);
            if(_item is Shield && _input.IsDown(_hands.UseItemInput(_item)))
                _input.ConsumeInput(_hands.UseItemInput(_item));
            base.Enter();
        }

        protected override bool CanTransitionTo()
        {
            var isShieldWithUseItemInput = _item is Shield && (_input.IsDown(_hands.UseItemInput(_item)) || _input.IsHeld(_hands.UseItemInput(_item)));
            var isDedicatedBlockerWithBlockInput = _hands.IsDedicatedToBlock(_item) && (_input.IsDown(InputKey.Block) || _input.IsHeld(InputKey.Block));
            return  base.CanTransitionTo() && (isShieldWithUseItemInput || isDedicatedBlockerWithBlockInput);
        }
    }
}