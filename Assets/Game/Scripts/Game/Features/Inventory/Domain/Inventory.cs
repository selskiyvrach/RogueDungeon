using System;
using System.Collections.Generic;
using System.Linq;
using Game.Libs.Items;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Features.Inventory.Domain
{
    public class Inventory
    {
        private readonly List<ItemContainer> _itemContainers = new (){
            
            new GridSpaceItemContainer(10, 4, ContainerId.Backpack0),
            
            new SlotItemContainer(SlotCategory.Handheld, ContainerId.LeftHand0),
            new SlotItemContainer(SlotCategory.Handheld, ContainerId.LeftHand1),
            new SlotItemContainer(SlotCategory.Handheld, ContainerId.LeftHand2),
            
            new SlotItemContainer(SlotCategory.Handheld, ContainerId.RightHand0),
            new SlotItemContainer(SlotCategory.Handheld, ContainerId.RightHand1),
            new SlotItemContainer(SlotCategory.Handheld, ContainerId.RightHand2),
        };

        private readonly CyclableSlotGroup[] _cyclableSlotGroups =
        {
            new(CyclableSlotGroupId.LeftHandItems, new [] {ContainerId.LeftHand0, ContainerId.LeftHand1, ContainerId.LeftHand2}, 0),
            new(CyclableSlotGroupId.RightHandItems, new[] { ContainerId.RightHand0, ContainerId.RightHand1, ContainerId.RightHand2 }, 0)
        };

        public event Action<ContainerId> OnContentChanged;
        public event Action<CyclableSlotGroupId> OnCyclableSlotIndexChanged;

        public void Equip(IItem item, ContainerId id)
        {
            var container = GetSlot(id);
            Assert.IsNull(container.PeekItem());
            container.PlaceItem((ISlotableItem)item);
            OnContentChanged?.Invoke(id);
        }

        public void CycleItemsInGroup(CyclableSlotGroupId group)
        {
            var slotGroup = _cyclableSlotGroups.First(n => n.SlotGroupId == group);
            var attemptsCount = slotGroup.ContainerIds.Length;
            while (attemptsCount-- > 0)
            {
                slotGroup.CurrentIndex++; 
                slotGroup.CurrentIndex %= slotGroup.ContainerIds.Length;
                if (GetCurrentItemFromCyclableSlot(group) == null) 
                    continue;
                
                OnCyclableSlotIndexChanged?.Invoke(group);
                break;
            }
        }

        public IHandheldItem GetCurrentHandheldItem(Hand hand) => 
            (IHandheldItem)GetCurrentItemFromCyclableSlot(hand.ToCyclableItemsGroup());

        private IItem GetCurrentItemFromCyclableSlot(CyclableSlotGroupId groupId)
        {
            var slotGroup = _cyclableSlotGroups.First(n => n.SlotGroupId == groupId);
            return GetSlot(slotGroup.ContainerIds[slotGroup.CurrentIndex]).PeekItem();
        }

        private SlotItemContainer GetSlot(ContainerId id) => 
            (SlotItemContainer)_itemContainers.First(n => n.Id == id);

        private class CyclableSlotGroup
        {
            public CyclableSlotGroupId SlotGroupId { get; }
            public ContainerId[] ContainerIds { get; }
            public int CurrentIndex { get; set; }

            public CyclableSlotGroup(CyclableSlotGroupId slotGroupId, ContainerId[] containerIds, int currentIndex)
            {
                SlotGroupId = slotGroupId;
                ContainerIds = containerIds;
                CurrentIndex = currentIndex;
            }
        }
    }
}