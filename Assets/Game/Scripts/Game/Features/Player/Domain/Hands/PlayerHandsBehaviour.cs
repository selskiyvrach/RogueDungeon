using System;
using Game.Libs.Input;
using Game.Libs.Items;
using UnityEngine.Assertions;
using Zenject;

namespace Game.Features.Player.Domain.Hands
{
    public class PlayerHandsBehaviour
    {
        public const string LEFT_HAND_INJECTION_ID = "left_hand";
        public const string RIGHT_HAND_INJECTION_ID = "right_hand";

        private readonly HandBehaviour _rightHand;
        private readonly HandBehaviour _leftHand;
        private bool _isHidden;

        public bool IsDoubleGrip => (_rightHand.CurrentItem == null || _leftHand.CurrentItem == null) && (_rightHand.CurrentItem ?? _leftHand.CurrentItem) != null;
        public bool IsIdle => _rightHand.IsIdle && _leftHand.IsIdle;
        
        public IHandheldItem LeftHandIntendedItem
        {
            set
            {
                Assert.IsFalse(_isHidden);
                _leftHand.IntendedItem = value;
            }
        }

        public IHandheldItem RightHandIntendedItem
        {
            set
            {
                Assert.IsFalse(_isHidden);
                _rightHand.IntendedItem = value;
            }
        }

        public event Action OnRefreshHandItemsRequested;
        public event Action OnLeftHandChangeItemRequested
        {
            add => _leftHand.OnCycleItemRequested += value;
            remove => _leftHand.OnCycleItemRequested -= value;
        }

        public event Action OnRightHandChangeItemRequested
        {
            add => _rightHand.OnCycleItemRequested += value;
            remove => _rightHand.OnCycleItemRequested -= value;
        }

        public PlayerHandsBehaviour(
            [Inject(Id = RIGHT_HAND_INJECTION_ID)] HandBehaviour rightHandBehaviour, 
            [Inject(Id = LEFT_HAND_INJECTION_ID)] HandBehaviour leftHandBehaviour)
        {
            _rightHand = rightHandBehaviour;
            _leftHand = leftHandBehaviour;
        }

        public void Initialize()
        {
            _rightHand.Initialize();
            _leftHand.Initialize();
        }

        public void Tick(float deltaTime)
        {
            _rightHand.Tick(deltaTime);
            _leftHand.Tick(deltaTime);
        }

        public HandBehaviour OppositeHand(IItem item) => 
            item == _rightHand.CurrentItem 
                ? _leftHand 
                : _rightHand;

        public HandBehaviour ThisHand(IItem item) => 
            item == _rightHand.CurrentItem 
                ? _rightHand 
                : _leftHand;

        public HandBehaviour OppositeHand(HandBehaviour handBehaviour) => 
            handBehaviour == _rightHand ? _leftHand : _rightHand;

        public bool IsInRightHand(IItem item) => 
            item == _rightHand.CurrentItem;

        public InputKey UseItemInput(IItem item) =>
            IsInRightHand(item)
                ? InputKey.UseRightHandItem
                : InputKey.UseLeftHandItem;

        public bool IsDedicatedToBlock(IItem item)
        {
            var thisPriority = (item as IBlockingItem)?.BlockingTier ?? BlockingTier.None;
            if(thisPriority == BlockingTier.None)
                return false;
            if (IsDoubleGrip)
                return true;
            
            var otherPriority = (OppositeHand(item).CurrentItem as IBlockingItem)?.BlockingTier ?? BlockingTier.None;
            if (thisPriority == otherPriority)
                return ThisHand(item) == _leftHand;
            
            return thisPriority > otherPriority;
        }

        public void Hide()
        {
            Assert.IsFalse(_isHidden);
            Assert.IsTrue(IsIdle);
            _leftHand.IntendedItem = null;
            _rightHand.IntendedItem = null;
            _isHidden = true;
        }

        public void Show()
        {
            _isHidden = false;
            OnRefreshHandItemsRequested?.Invoke();
        }
    }
}