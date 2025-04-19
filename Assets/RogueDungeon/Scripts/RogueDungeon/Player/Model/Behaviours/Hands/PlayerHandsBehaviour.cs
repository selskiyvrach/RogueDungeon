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

        public bool IsDualWieldingSameTypeItems() => 
            IsDualWieldingShields() || IsDualWieldingWeapons();

        public bool IsDualWieldingShields() => 
            RightHand.CurrentItem is Shield && LeftHand.CurrentItem is Shield;

        public bool IsDualWieldingWeapons() => 
            RightHand.CurrentItem is Weapon && LeftHand.CurrentItem is Weapon;

        public bool IsDedicatedToBlock(IItem item)
        {
            if (item is Shield && OppositeHand(item).CurrentItem is not Shield)
                return true;

            if (OppositeHand(item).CurrentItem is null && _playerInput.IsDown(UseItemInput(item)))
                return true;
            
            return IsDualWieldingSameTypeItems() && !IsInRightHand(item) || IsDoubleGrip;
        }
    }
}