using Common.Events;
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
        private readonly DiContainer _container;

        public GameplayGameState(ISceneLoader sceneLoader, DiContainer container)
        {
            _sceneLoader = sceneLoader;
            _container = container;
        }

        public async void Enter()
        {
            _container.Bind<ISceneContainerReadyListener<GameplayScene>>().FromInstance(this).AsSingle();
            await _sceneLoader.Load<GameplayScene>();
            _container.Unbind<ISceneContainerReadyListener<GameplayScene>>();
        }

        public void OnSceneContainerReady(DiContainer container)
        {
            
        }

        public void Exit() =>
            _container.Unbind<ISceneContainerReadyListener<GameplayScene>>();
    }
}