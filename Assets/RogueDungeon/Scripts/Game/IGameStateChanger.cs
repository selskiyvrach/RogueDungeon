namespace RogueDungeon.Game
{
    public interface IGameStateChanger
    {
        void EnterState<T>() where T : IGameState;
    }
}