namespace Game.Features.Player.Domain.Behaviours.Hands
{
    public interface IBlockingItem : IItem
    {
        BlockingTier BlockingTier { get; }
    }
}