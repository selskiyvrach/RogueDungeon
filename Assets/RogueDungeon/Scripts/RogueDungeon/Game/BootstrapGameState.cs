using Common.Game;
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
            container.BindInstance(this);
        }

        public void BoostrapInstallerIsReady(BootstrapStateInstaller installer)
        {
            installer.Bootstrap(_container);
            _gameStateChanger.EnterState<MainMenuGameState>();
        }

        public void Enter()
        {
            
        }
    }
}