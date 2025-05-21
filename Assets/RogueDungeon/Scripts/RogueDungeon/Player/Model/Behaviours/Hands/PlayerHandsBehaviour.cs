using RogueDungeon.Input;
using RogueDungeon.Items;
using UnityEngine.Assertions;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class PlayerHandsBehaviour 
    {
        private Inventory.Inventory _inventory;

        public bool TransitionsLocked { get; private set; }
        public PlayerHandBehaviour RightHand { get; private set; }
        public PlayerHandBehaviour LeftHand { get; private set; }
        public bool IsDoubleGrip => (RightHand.CurrentItem == null || LeftHand.CurrentItem == null) && (RightHand.CurrentItem ?? LeftHand.CurrentItem) != null;
        public bool IsIdle => RightHand.IsIdleOrEmpty && LeftHand.IsIdleOrEmpty;

        public PlayerHandsBehaviour(Inventory.Inventory inventory) => 
            _inventory = inventory;

        public void SetBehaviours(PlayerHandBehaviour rightHandBehaviour, PlayerHandBehaviour leftHandBehaviour)
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
        }
        
        public void Disable(bool force = false)
        {
            Assert.IsTrue(force || IsIdle);
            TransitionsLocked = LeftHand.IsLocked = RightHand.IsLocked = true;
        }

        public void Enable() => 
            TransitionsLocked = LeftHand.IsLocked = RightHand.IsLocked = false;

        public PlayerHandBehaviour OppositeHand(IItem item) => 
            item == RightHand.CurrentItem 
                ? LeftHand 
                : RightHand;

        public PlayerHandBehaviour ThisHand(IItem item) => 
            item == RightHand.CurrentItem 
                ? RightHand 
                : LeftHand;

        public PlayerHandBehaviour OppositeHand(PlayerHandBehaviour handBehaviour) => 
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