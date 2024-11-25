namespace Common.Game
{
    public interface IGameStatesFactory
    {
        T Create<T>() where T : IGameState;
    }
}