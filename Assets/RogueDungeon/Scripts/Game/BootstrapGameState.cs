using Zenject;

namespace RogueDungeon.Game
{
    public class BootstrapGameState : IGameState
    {
        private readonly IGameStateChanger _gameStateChanger;
        private readonly DiContainer _diContainer;

        public BootstrapGameState(IGameStateChanger gameStateChanger, DiContainer diContainer)
        {
            _gameStateChanger = gameStateChanger;
            _diContainer = diContainer;
        }

        public void Enter()
        {
            _diContainer.Resolve<BootstrapStateInstaller>().InstallBindings();
            _gameStateChanger.EnterState<MainMenuGameState>();
        }
    }
}