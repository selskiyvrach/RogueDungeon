using System;
using Game.Features.Inventory.Shared;
using Game.Features.Items.Domain;
using Game.Features.Items.Domain.Moves;
using Game.Features.Items.Domain.Wielder;
using Game.Libs.Input;
using Libs.Fsm;
using Libs.Lifecycle;
using UnityEngine.Assertions;

namespace Game.Features.Player.Domain.Behaviours.Hands
{
    public class HandBehaviour : ITickable, IInitializable, IItemSwapper
    {
        private readonly ItemMoveSetFactory _moveSetFactory;
        private readonly IPlayerInput _playerInput;
        private readonly Inventory.Domain.Inventory _inventory;
        private readonly SlotType[] _slots;
        private readonly InputUnit _cycleItemsKey;

        private int _currentItemIndex;

        private StateMachine _currentItemMoveset;
        private IHandheldItem _currentItem;
        private IHandheldItem _intendedItem;
        public event Action OnCurrentItemChanged;

        public bool IsRightHand { get; }
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
                
                _currentItem = value;
                if (_currentItem != null)
                {
                    _currentItemMoveset = _moveSetFactory.Create(_currentItem);
                    _currentItemMoveset.Initialize();
                }
                else
                    _currentItemMoveset = null;

                OnCurrentItemChanged?.Invoke();
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

        public bool IsIdleOrEmpty => _currentItem == null || _currentItemMoveset.CurrentState is ItemIdleMove;

        public HandBehaviour(IPlayerInput playerInput, Inventory.Domain.Inventory inventory, bool isRightHand, ItemMoveSetFactory moveSetFactory)
        {
            _playerInput = playerInput;
            IsRightHand = isRightHand;
            _moveSetFactory = moveSetFactory;
            _inventory = inventory;
            _slots = new[]
            {
                IsRightHand ? SlotType.HandheldRight1 : SlotType.HandheldLeft1,
                IsRightHand ? SlotType.HandheldRight2 : SlotType.HandheldLeft2,
                IsRightHand ? SlotType.HandheldRight3 : SlotType.HandheldLeft3,
            };
            _cycleItemsKey = _playerInput.GetKey(IsRightHand ? InputKey.CycleRightArmItems : InputKey.CycleLeftArmItems);
        }

        public void Tick(float deltaTime)
        {
            _currentItemMoveset?.Tick(deltaTime);
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
            var attempts = _slots.Length;
            while (attempts-- > 0)
            {
                _currentItemIndex++;
                _currentItemIndex %= attempts;
                if (GetItem(_currentItemIndex) is not { } item)
                    continue;
                IntendedItem = item;
                break;
            }
        }

        private IHandheldItem GetItem(int index) =>
            _inventory.GetItem(_slots[index]) as IHandheldItem;
    }
}