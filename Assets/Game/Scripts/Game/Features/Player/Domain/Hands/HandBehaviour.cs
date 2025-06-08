using System;
using Game.Libs.Input;
using Game.Libs.Items;
using Libs.Fsm;
using Libs.Lifecycle;
using UnityEngine.Assertions;

namespace Game.Features.Player.Domain.Behaviours.Hands
{
    public class HandBehaviour : ITickable, IInitializable
    {
        private readonly IInventory _inventory;
        private readonly InputUnit _cycleItemsKey;

        private int _currentItemIndex;

        private StateMachine _currentItemMoveset;
        private IItem _currentItem;
        private IItem _intendedItem;
        
        public event Action OnCurrentItemChanged;

        public bool IsRightHand { get; }
        public bool IsLocked { get; set; }

        public IItem CurrentItem
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
                OnCurrentItemChanged?.Invoke();
            }
        }

        public IItem IntendedItem
        {
            get => _intendedItem;
            set
            {
                Assert.IsFalse(IsLocked);
                if(!IsLocked)
                    _intendedItem = value;
            }
        }
        
        public HandBehaviour(IPlayerInput playerInput, bool isRightHand)
        {
            IsRightHand = isRightHand;
            _cycleItemsKey = playerInput.GetKey(IsRightHand ? InputKey.CycleRightArmItems : InputKey.CycleLeftArmItems);
        }

        public void Initialize()
        {
            IntendedItem = _inventory.GetEquippedItem(isRightHand: IsRightHand);
            _cycleItemsKey.OnDown += NextItem;
        }

        public void Tick(float deltaTime)
        {
            _currentItemMoveset?.Tick(deltaTime);
            if(CurrentItem == null && IntendedItem != null)
                CurrentItem = IntendedItem; 
        }

        private void NextItem()
        {
            _cycleItemsKey.Reset();
            _inventory.CycleEquippedItem(isRightHand: IsRightHand);
            IntendedItem = _inventory.GetEquippedItem(isRightHand: IsRightHand);
        }
    }
}