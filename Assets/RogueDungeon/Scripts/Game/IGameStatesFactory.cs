namespace RogueDungeon.Game
{
    public interface IGameStatesFactory
    {
        T Create<T>() where T : IGameState;
    }
}