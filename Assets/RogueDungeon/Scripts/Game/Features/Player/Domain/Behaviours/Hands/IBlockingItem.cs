namespace Game.Features.Player.Domain.Behaviours.Hands
{
    public interface IBlockingItem
    {
        BlockingTier BlockingTier { get; }
    }
}