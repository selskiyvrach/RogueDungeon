using Common.Game;
using Common.SceneManagement;
using RogueDungeon.UI.LoadingScreen;
using Zenject;

namespace RogueDungeon.Game
{
    public class BootstrapGameState : IGameState
    {
        private readonly IGameStateChanger _gameStateChanger;
        private readonly DiContainer _container;

        public BootstrapGameState(IGameStateChanger gameStateChanger, DiContainer container)
        {
            _gameStateChanger = gameStateChanger;
            _container = container;
            _container.Bind<BootstrapGameState>().FromInstance(this);
        }

        public void BoostrapInstallerIsReady(BootstrapperInstaller installer)
        {
            installer.Bootstrap(_container);
            _gameStateChanger.EnterState<MainMenuGameState>();
            _container.Unbind<BootstrapGameState>();
        }

        public void Enter()
        {
        }   
    }
}