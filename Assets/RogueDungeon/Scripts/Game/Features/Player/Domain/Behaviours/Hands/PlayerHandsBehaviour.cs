using Game.Libs.Input;
using UnityEngine.Assertions;

namespace Game.Features.Player.Domain.Behaviours.Hands
{
    public class PlayerHandsBehaviour 
    {
        private readonly IInventory _inventory;
        private readonly IPlayerInput _input;
        private readonly InputUnit _mapKey;
        
        private IItem _previousRightHandItem;

        public bool TransitionsLocked { get; private set; }
        public HandBehaviour RightHand { get; private set; }
        public HandBehaviour LeftHand { get; private set; }
        public bool IsDoubleGrip => (RightHand.CurrentItem == null || LeftHand.CurrentItem == null) && (RightHand.CurrentItem ?? LeftHand.CurrentItem) != null;
        public bool IsIdle => RightHand.IsIdleOrEmpty && LeftHand.IsIdleOrEmpty;

        public PlayerHandsBehaviour(IPlayerInput input, IInventory inventory)
        {
            _input = input;
            _inventory = inventory;
            _mapKey = _input.GetKey(InputKey.OpenMap);
        }

        public void SetBehaviours(HandBehaviour rightHandBehaviour, HandBehaviour leftHandBehaviour)
        {
            RightHand = rightHandBehaviour;
            LeftHand = leftHandBehaviour;
        }

        public void Initialize()
        {
            RightHand.Initialize();
            LeftHand.Initialize();
        }

        public void Tick(float deltaTime)
        {
            RightHand.Tick(deltaTime);
            LeftHand.Tick(deltaTime);
            
                     
            if(!_mapKey.IsDown)
                return;
            
            var map = _inventory.GetMapItem();
            
            if (RightHand.CurrentItem != map)
            {
                _previousRightHandItem = RightHand.CurrentItem;
                RightHand.IntendedItem = map;
            }
            else if (RightHand.CurrentItem == map) 
                RightHand.IntendedItem = _previousRightHandItem;
            
            _mapKey.Reset();
        }
        
        public void Disable(bool force = false)
        {
            Assert.IsTrue(force || IsIdle);
            TransitionsLocked = LeftHand.IsLocked = RightHand.IsLocked = true;
        }

        public void Enable() => 
            TransitionsLocked = LeftHand.IsLocked = RightHand.IsLocked = false;

        public HandBehaviour OppositeHand(IItem item) => 
            item == RightHand.CurrentItem 
                ? LeftHand 
                : RightHand;

        public HandBehaviour ThisHand(IItem item) => 
            item == RightHand.CurrentItem 
                ? RightHand 
                : LeftHand;

        public HandBehaviour OppositeHand(HandBehaviour handBehaviour) => 
            handBehaviour == RightHand ? LeftHand : RightHand;

        public bool IsInRightHand(IItem item) => 
            item == RightHand.CurrentItem;

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
                return ThisHand(item) == LeftHand;
            
            return thisPriority > otherPriority;
        }
    }
}