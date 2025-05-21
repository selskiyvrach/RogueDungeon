using Common.Lifecycle;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Attacks;
using RogueDungeon.Player.Model.Inventory;
using UnityEngine.Assertions;
using ITickable = Common.Lifecycle.ITickable;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class PlayerHandBehaviour : ITickable, IInitializable
    {
        private readonly HandheldItemFactory _factory;
        private readonly IPlayerInput _playerInput;
        private readonly Inventory.Inventory _inventory;
        private readonly SlotType[] _slots;
        public bool IsRightHand { get; }

        private int _currentItemIndex;

        private IHandheldItem _currentItem;
        private IHandheldItem _intendedItem;
        private InputUnit _cycleItemsKey;

        public bool IsLocked { get; set; }

        public IHandheldItem CurrentItem
        {
            get => _currentItem;
            set
            {
                Assert.IsFalse(IsLocked);
                if(IsLocked)
                    return;
                
                if (value == _currentItem)
                    return;
                
                if(_currentItem != null)
                    _factory.DestroyItemPresenter(_currentItem);
                
                if(value != null)
                {
                    _factory.CreateItemPresenter(value);
                    value.Moveset.Initialize();
                }
                
                _currentItem = value;
            }
        }

        public IHandheldItem IntendedItem
        {
            get => _intendedItem;
            set
            {
                Assert.IsFalse(IsLocked);
                if(!IsLocked)
                    _intendedItem = value;
            }
        }

        public bool IsIdleOrEmpty => _currentItem == null || _currentItem.Moveset.CurrentState is ItemIdleMove;

        public PlayerHandBehaviour(HandheldItemFactory factory, IPlayerInput playerInput, Inventory.Inventory inventory, bool isRightHand)
        {
            _factory = factory;
            _playerInput = playerInput;
            _cycleItemsKey = _playerInput.GetKey(IsRightHand ? InputKey.CycleRightArmItems : InputKey.CycleLeftArmItems);
            _inventory = inventory;
            IsRightHand = isRightHand;
            _slots = new[]
            {
                IsRightHand ? SlotType.HandheldRight1 : SlotType.HandheldLeft1,
                IsRightHand ? SlotType.HandheldRight2 : SlotType.HandheldLeft2,
                IsRightHand ? SlotType.HandheldRight3 : SlotType.HandheldLeft3,
            };
        }

        public void Tick(float deltaTime)
        {
            _currentItem?.Moveset?.Tick(deltaTime);
            if(CurrentItem == null && IntendedItem != null)
                CurrentItem = IntendedItem; 
        }

        public void Initialize()
        {
            IntendedItem = GetItem(_currentItemIndex);
            _cycleItemsKey.OnDown += NextItem;
        }

        private void NextItem()
        {
            _cycleItemsKey.Reset();
            var attemtps = _slots.Length;
            while (attemtps-- > 0)
            {
                if (GetItem(++_currentItemIndex) is not { } item)
                    continue;
                IntendedItem = item;
                break;
            }
        }

        private IHandheldItem GetItem(int index) =>
            (IHandheldItem)_inventory.GetItem(_slots[index]);
    }
}