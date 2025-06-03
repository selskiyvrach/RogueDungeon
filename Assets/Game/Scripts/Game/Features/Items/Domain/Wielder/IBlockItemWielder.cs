namespace Game.Features.Items.Domain.Wielder
{
    public interface IBlockItemWielder : IItemTransitionsLockedProvider, IItemInputKeyProvider, IStaminaConsumingItemWielder
    {
        bool HasUnabsorbedBlockImpact { get; set; }
        bool IsDedicatedBlockItem(IItem item);
        IBlockingItem BlockingItem { get; set; }
    }
}