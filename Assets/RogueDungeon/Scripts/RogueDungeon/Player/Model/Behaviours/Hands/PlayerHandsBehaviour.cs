using RogueDungeon.Items;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class PlayerHandsBehaviour 
    {
        public PlayerHandBehaviour RightHand { get; private set; }
        public PlayerHandBehaviour LeftHand { get; private set; }
 
        public bool IsDoubleGrip => RightHand.CurrentItem == null || LeftHand.CurrentItem == null;

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

        public bool IsDedicatedBlockingItem(IItem item) => 
            IsDoubleGrip || OppositeHand(item).CurrentItem.Config.BlockStaminaCostMultiplier >= item.Config.BlockStaminaCostMultiplier;
    }
}