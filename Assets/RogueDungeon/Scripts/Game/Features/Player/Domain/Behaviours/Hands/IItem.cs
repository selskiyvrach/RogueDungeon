using Game.Features.Player.Domain.Behaviours.Hands;

public interface IItem
{
    string Id { get; }
    bool IsIdle { get; }
    void EnableMoveset();
    void DisableMoveset();
}

public interface IBlockingItem : IItem
{
    BlockingTier BlockingTier { get; }
}