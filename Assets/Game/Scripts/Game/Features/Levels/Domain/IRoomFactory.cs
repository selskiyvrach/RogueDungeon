namespace Game.Features.Levels.Domain
{
    public interface IRoomFactory
    {
        Room Create(IRoomConfig config);
    }
}