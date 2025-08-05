using System;
using Game.Features.Player.Domain.Movesets.Items;
using Game.Libs.Input;
using Game.Libs.Items;
using Libs.Fsm;
using Libs.Lifecycle;
using UnityEngine.Assertions;

namespace Game.Features.Player.Domain.Behaviours.Hands
{
    public class HandBehaviour : ITickable, IInitializable
    {
        private readonly InputUnit _cycleItemsKey;
        private readonly ItemMovesetFactory _itemMovesetFactory;

        private int _currentItemIndex;
        private StateMachine _currentItemMoveset;
        private IHandheldItem _currentItem;
        private IHandheldItem _intendedItem;

        public event Action OnCycleItemRequested;
        public event Action OnCurrentItemChanged;

        public bool IsRightHand { get; }
        
        public IHandheldItem CurrentItem
        {
            get => _currentItem;
            set
            {
                if (value == _currentItem)
                    return;

                _currentItem = value;

                if (_currentItem == null)
                    _currentItemMoveset = null;
                else
                {
                    _currentItemMoveset = _itemMovesetFactory.Create(_currentItem);
                    _currentItemMoveset.Initialize();
                }
                OnCurrentItemChanged?.Invoke();
            }
        }

        public IHandheldItem IntendedItem
        {
            get => _intendedItem;
            set
            {
                Assert.IsFalse(IsHidden);
                _intendedItem = value;
            }
        }

        public bool IsCurrentItemIdle => _currentItemMoveset?.CurrentState is ItemIdleMove;
        public bool IsIdle => _currentItem == null || IsCurrentItemIdle;
        public bool IsHidden { get; private set; }

        public HandBehaviour(IPlayerInput playerInput, bool isRightHand, ItemMovesetFactory itemMovesetFactory)
        {
            IsRightHand = isRightHand;
            _itemMovesetFactory = itemMovesetFactory;
            _cycleItemsKey = playerInput.GetKey(IsRightHand ? InputKey.CycleRightArmItems : InputKey.CycleLeftArmItems);
        }

        public void Initialize() => 
            _cycleItemsKey.OnDown += NextItem;

        public void Tick(float deltaTime)
        {
            _currentItemMoveset?.Tick(deltaTime);
            
            if(IntendedItem == CurrentItem)
                return;
            if(IntendedItem != null && CurrentItem == null)
                CurrentItem = IntendedItem; 
        }

        private void NextItem()
        {
            _cycleItemsKey.Reset();
            OnCycleItemRequested?.Invoke();
        }
    }
}