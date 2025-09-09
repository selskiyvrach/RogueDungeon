using Game.Features.Player.Domain.Hands;
using Game.Features.Player.Domain.Movesets.Items.Interfaces;
using Game.Libs.Items;

namespace Game.Features.Player.Domain
{
    public class HandToItemSwapperAdapter : IItemSwapper
    {
        private readonly HandBehaviour _hand;

        public IHandheldItem CurrentItem
        {
            get => _hand.CurrentItem;
            set => _hand.CurrentItem = value;
        }

        public IHandheldItem IntendedItem
        {
            get => _hand.IntendedItem;
            set => _hand.IntendedItem = value;
        }

        public bool CanSheathCurrentItem => _hand.IsCurrentItemIdle;

        public HandToItemSwapperAdapter(HandBehaviour hand) => 
            _hand = hand;
    }
}