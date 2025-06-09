using Game.Features.Player.Domain.Movesets.Items.Interfaces;
using Game.Libs.InGameResources;
using Game.Libs.Input;
using Game.Libs.Items;

namespace Game.Features.Player.Domain
{
    public class PlayerToItemMovesetFacade : IAttackItemWielder, IBlockItemWielder
    {
        private readonly IPlayerAttacksMediator _attacksMediator;
        private readonly Player _player;
        public bool ItemTransitionsAreLocked { get; set; }
        public bool IsAttackInUncancellableState { get; set; }
        public bool HasUnabsorbedBlockImpact { get; set; }
        public IResource Stamina => _player.Stamina;
        public bool CanAttack { get; set; }
        public IBlockingItem BlockingItem { get; set; }

        public PlayerToItemMovesetFacade(Player player, IPlayerAttacksMediator attacksMediator)
        {
            _player = player;
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