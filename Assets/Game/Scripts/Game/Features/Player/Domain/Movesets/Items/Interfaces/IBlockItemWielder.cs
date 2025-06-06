using Game.Libs.Items;

namespace Game.Features.Player.Domain.Movesets.Items.Interfaces
{
    public interface IBlockItemWielder : IItemTransitionsLockedProvider, IItemInputKeyProvider, IStaminaConsumingItemWielder
    {
        bool HasUnabsorbedBlockImpact { get; set; }
        bool IsDedicatedBlockItem(IItem item);
        IBlockingItem BlockingItem { get; set; }
    }
}