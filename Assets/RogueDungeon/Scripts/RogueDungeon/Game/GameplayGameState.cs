using Common.FSM;
using Common.Game;
using Common.SceneManagement;
using RogueDungeon.SceneManagement;
using Zenject;

namespace RogueDungeon.Game
{
    internal class GameplayGameState : IGameState, ISceneContainerReadyListener<GameplayScene>, IExitable
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly DiContainer _globalContainer;

        public GameplayGameState(ISceneLoader sceneLoader, DiContainer globalContainer)
        {
            _sceneLoader = sceneLoader;
            _globalContainer = globalContainer;
        }

        public async void Enter()
        {
            _globalContainer.Bind<ISceneContainerReadyListener<GameplayScene>>().FromInstance(this).AsSingle();
            await _sceneLoader.Load<GameplayScene>();
            _globalContainer.Unbind<ISceneContainerReadyListener<GameplayScene>>();
        }

        public void OnSceneContainerReady(DiContainer sceneContainer)
        {
            var gameplayInstaller = _globalContainer.Resolve<GameplayStateInstaller>();
            gameplayInstaller.InstallToSceneContext(sceneContainer);
        }

        public void Exit() =>
            _globalContainer.Unbind<ISceneContainerReadyListener<GameplayScene>>();
    }
}