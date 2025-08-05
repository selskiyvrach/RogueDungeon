using System;
using Game.Features.Inventory.Domain;
using Game.Features.Player.Domain.Behaviours.Hands;

namespace Game.Features.Player.App.UseCases.Instance
{
    public class SyncDrawnWeaponsWithInventoryStateUseCase
    {
        private readonly Inventory.Domain.Inventory _inventory;
        private readonly PlayerHandsBehaviour _hands;

        public SyncDrawnWeaponsWithInventoryStateUseCase(Inventory.Domain.Inventory inventory, PlayerHandsBehaviour hands)
        {
            _inventory = inventory;
            _hands = hands;
            _inventory.OnCurrentHandheldItemChanged += ReflectHandContentChange;
            _hands.OnLeftHandChangeItemRequested += () => _inventory.CycleItemsInGroup(CyclableItemsGroup.LeftHand);
            _hands.OnRightHandChangeItemRequested += () => _inventory.CycleItemsInGroup(CyclableItemsGroup.RightHand);
            _hands.OnRefreshHandItemsRequested += RefreshHandsItems;
            RefreshHandsItems();
        }

        private void ReflectHandContentChange(Hand hand)
        {
            switch (hand)
            {
                case Hand.Right:
                    _hands.RightHandIntendedItem = _inventory.GetCurrentHandheldItem(hand);
                    break;
                case Hand.Left:
                    _hands.LeftHandIntendedItem = _inventory.GetCurrentHandheldItem(hand);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(hand), hand, null);
            }
        }

        private void RefreshHandsItems()
        {
            ReflectHandContentChange(Hand.Right);
            ReflectHandContentChange(Hand.Left);
        }
    }
}