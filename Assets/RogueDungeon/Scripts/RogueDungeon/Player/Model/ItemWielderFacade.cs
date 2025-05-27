using Characters;
using Input;
using RogueDungeon.Items.Model;

namespace Player.Model
{
    public class ItemWielderFacade : IAttackItemWielder, IBlockItemWielder
    {
        private readonly IPlayerAttacksMediator _attacksMediator;
        private readonly PlayerControlStateMediator _controlState;
        private readonly Player _player;

        public bool ItemTransitionsAreLocked => _player.Hands.TransitionsLocked;
        public IResource Stamina => _player.Stamina;

        public bool IsAttackInUncancellableState
        {
            set => _controlState.IsAttackInUncancellableState = value;
        }

        public bool CanAttack => _controlState.CanAttack;

        public bool HasUnabsorbedBlockImpact
        {
            get => _player.HasUnabsorbedBlockImpact;
            set => _player.HasUnabsorbedBlockImpact = value;
        }

        public IBlockingItem BlockingItem
        {
            get => _player.BlockingItem;
            set => _player.BlockingItem = value;
        }

        public ItemWielderFacade(Player player, PlayerControlStateMediator controlState, IPlayerAttacksMediator attacksMediator)
        {
            _player = player;
            _controlState = controlState;
            _attacksMediator = attacksMediator;
        }

        public InputKey GetInputKeyForItem(IItem item) => 
            _player.Hands.UseItemInput(item);

        public void PerformAttack(IWeapon weapon) => 
            _attacksMediator.MediatePlayerAttack(weapon);

        public bool IsDedicatedBlockItem(IItem item) => 
            _player.Hands.IsDedicatedToBlock(item);
    }
}