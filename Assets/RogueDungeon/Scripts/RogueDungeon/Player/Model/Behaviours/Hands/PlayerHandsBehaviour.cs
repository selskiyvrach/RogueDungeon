using RogueDungeon.Input;
using RogueDungeon.Items;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class PlayerHandsBehaviour 
    {
        private readonly IPlayerInput _playerInput;

        public PlayerHandBehaviour RightHand { get; private set; }
        public PlayerHandBehaviour LeftHand { get; private set; } 

        public bool IsDoubleGrip => (RightHand.CurrentItem == null || LeftHand.CurrentItem == null) && (RightHand.CurrentItem ?? LeftHand.CurrentItem) != null;

        public PlayerHandsBehaviour(IPlayerInput playerInput) => 
            _playerInput = playerInput;

        public void SetBehaviours(PlayerHandBehaviour rightHandBehaviour, PlayerHandBehaviour leftHandBehaviour)
        {
            RightHand = rightHandBehaviour;
            LeftHand = leftHandBehaviour;
        }

        public void Initialize()
        {
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
        
        public bool IsInRightHand(IItem item) => 
            item == RightHand.CurrentItem;

        public InputKey UseItemInput(IItem item) =>
            IsInRightHand(item)
                ? InputKey.UseRightHandItem
                : InputKey.UseLeftHandItem;
        
        public bool IsDedicatedToBlock(IItem item)
        {
            var thisPriority = (item as IBlockingItem)?.BlockingTier ?? BlockingTier.None;
            var otherPriority = (OppositeHand(item).CurrentItem as IBlockingItem)?.BlockingTier ?? BlockingTier.None;
            return thisPriority > otherPriority || IsDoubleGrip || ThisHand(item) == LeftHand;
        }
    }
}