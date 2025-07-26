using Game.Libs.Input;
using Game.Libs.Items;
using UnityEngine.Assertions;
using Zenject;

namespace Game.Features.Player.Domain.Behaviours.Hands
{
    public class PlayerHandsBehaviour
    {
        public const string LEFT_HAND_INJECTION_ID = "left_hand";
        public const string RIGHT_HAND_INJECTION_ID = "right_hand";

        public HandBehaviour RightHand { get; }
        public HandBehaviour LeftHand { get; }
        public bool IsDoubleGrip => (RightHand.CurrentItem == null || LeftHand.CurrentItem == null) && (RightHand.CurrentItem ?? LeftHand.CurrentItem) != null;
        public bool IsIdle => RightHand.IsIdle && LeftHand.IsIdle;

        public PlayerHandsBehaviour(
            [Inject(Id = RIGHT_HAND_INJECTION_ID)] HandBehaviour rightHandBehaviour, 
            [Inject(Id = LEFT_HAND_INJECTION_ID)] HandBehaviour leftHandBehaviour)
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
            LeftHand.IsLocked = RightHand.IsLocked = true;
        }

        public void Enable() => 
            LeftHand.IsLocked = RightHand.IsLocked = false;

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