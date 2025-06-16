using Game.Features.Player.Domain.Movesets.Items.Interfaces;
using Game.Libs.InGameResources;
using Game.Libs.Input;
using Game.Libs.Items;

namespace Game.Features.Player.Domain
{
    public class PlayerToItemMovesetFacade : IAttackItemWielder, IBlockItemWielder
    {
        private readonly Player _player;
        public bool ItemTransitionsAreLocked { get; set; }
        public bool IsAttackInUncancellableState { get; set; }
        public bool HasUnabsorbedBlockImpact
        {
            get => _player.HasUnabsorbedBlockImpact;
            set => _player.HasUnabsorbedBlockImpact = value;
        }

        public IResource Stamina => _player.Stamina;
        public bool CanAttack { get; set; }
        public IBlockingItem BlockingItem
        {
            get => _player.BlockingItem;
            set => _player.BlockingItem = value;
        }

        public PlayerToItemMovesetFacade(Player player) => 
            _player = player;

        public InputKey GetInputKeyForItem(IItem item) => 
            _player.Hands.UseItemInput(item);

        public void PerformAttack(IWeapon weapon) => 
            _player.PerformAttack(weapon);

        public bool IsDedicatedBlockItem(IItem item) => 
            _player.Hands.IsDedicatedToBlock(item);
    }
}