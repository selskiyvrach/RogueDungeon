using RogueDungeon.Game;

namespace Common.Game
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