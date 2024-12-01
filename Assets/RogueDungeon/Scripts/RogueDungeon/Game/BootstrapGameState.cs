using Common.Game;
using Zenject;

namespace RogueDungeon.Game
{
    public class BootstrapGameState : IGameState
    {
        private readonly IGameStateChanger _gameStateChanger;

        public BootstrapGameState(IGameStateChanger gameStateChanger, DiContainer container, BootstrapStateInstaller stateInstaller)
        {
            _gameStateChanger = gameStateChanger;
            stateInstaller.Install(container);
        }

        public void Enter() => 
            _gameStateChanger.EnterState<MainMenuGameState>();
    }
}