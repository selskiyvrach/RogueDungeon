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
            _inventory.OnCyclableSlotIndexChanged += ReflectHandContentChange;
            _hands.OnLeftHandChangeItemRequested += () => _inventory.CycleItemsInGroup(CyclableSlotGroupId.LeftHandItems);
            _hands.OnRightHandChangeItemRequested += () => _inventory.CycleItemsInGroup(CyclableSlotGroupId.RightHandItems);
            _hands.OnRefreshHandItemsRequested += RefreshHandsItems;
            RefreshHandsItems();
        }

        private void ReflectHandContentChange(CyclableSlotGroupId cyclableSlotGroupId)
        {
            switch (cyclableSlotGroupId)
            {
                case CyclableSlotGroupId.RightHandItems:
                    _hands.RightHandIntendedItem = _inventory.GetCurrentHandheldItem(Hand.Right);
                    break;
                case CyclableSlotGroupId.LeftHandItems:
                    _hands.LeftHandIntendedItem = _inventory.GetCurrentHandheldItem(Hand.Left);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cyclableSlotGroupId), cyclableSlotGroupId, null);
            }
        }

        private void RefreshHandsItems()
        {
            ReflectHandContentChange(CyclableSlotGroupId.RightHandItems);
            ReflectHandContentChange(CyclableSlotGroupId.LeftHandItems);
        }
    }
}