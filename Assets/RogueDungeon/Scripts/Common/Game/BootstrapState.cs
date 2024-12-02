using System.Threading.Tasks;

namespace Common.Game
{
    public class BootstrapState : GameState
    {
        private readonly IGameStateChanger _gameStateChanger;

        public BootstrapState(IGameStateChanger gameStateChanger) => 
            _gameStateChanger = gameStateChanger;

        public override Task Enter()
        {
            _gameStateChanger.EnterState<MainMenuState>();
            return Task.CompletedTask;
        }
    }
}