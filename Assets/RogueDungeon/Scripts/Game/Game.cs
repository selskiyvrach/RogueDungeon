namespace RogueDungeon.Game
{
    public class Game
    {
        private readonly IGameStateChanger _stateChanger;

        public Game(IGameStateChanger stateChanger)
        {
            _stateChanger = stateChanger;
            _stateChanger.EnterState<BootstrapGameState>();
        }
    }
}