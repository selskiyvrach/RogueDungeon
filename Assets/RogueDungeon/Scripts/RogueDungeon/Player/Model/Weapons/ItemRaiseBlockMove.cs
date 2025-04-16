using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours;
using RogueDungeon.Player.Model.Behaviours.Hands;
using UnityEngine;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemRaiseBlockMove : PlayerMove
    {
        private readonly IItem _item;
        private readonly PlayerHandsBehaviour _hands;
        private readonly IPlayerInput _input;

        protected override float Duration => _item.Config.RaiseBlockDuration;

        protected ItemRaiseBlockMove(IAnimation animation, PlayerHandsBehaviour hands,
            IItem item, string id, IPlayerInput input) : base(id, animation)
        {
            _hands = hands;
            _item = item;
            _input = input;
        }

        protected override bool CanTransitionTo()
        {
            var isShieldWithUseItemInput = _item is Shield && (_input.HasInput(_hands.UseItemInput(_item)) || !_input.IsInputUp(_hands.UseItemInput(_item)));
            var isDedicatedBlockerWithBlockInput = _hands.IsDedicatedToBlock(_item) && (_input.HasInput(InputKey.Block) || !_input.IsInputUp(InputKey.Block));
            return  base.CanTransitionTo() && (isShieldWithUseItemInput || isDedicatedBlockerWithBlockInput);
        }
    }
}