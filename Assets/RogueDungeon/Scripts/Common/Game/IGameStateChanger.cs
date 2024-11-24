namespace Common.Game
{
    public interface IGameStateChanger
    {
        void EnterState<T>() where T : IGameState;
    }
}