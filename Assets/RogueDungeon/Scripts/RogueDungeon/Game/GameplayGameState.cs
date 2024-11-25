using Common.Events;
using Common.FSM;
using Common.Game;
using Common.SceneManagement;
using RogueDungeon.SceneManagement;
using Zenject;

namespace RogueDungeon.Game
{
    internal class GameplayGameState : IGameState, IEventHandler<OnSceneContainerCreatedEvent<GameplayScene>>, IExitable
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
            _container.Bind<IEventHandler<OnSceneContainerCreatedEvent<GameplayScene>>>().FromInstance(this).AsSingle();
            await _sceneLoader.Load<GameplayScene>();
            _container.Unbind<IEventHandler<OnSceneContainerCreatedEvent<GameplayScene>>>();
        }

        public void HandleEvent(OnSceneContainerCreatedEvent<GameplayScene> @event)
        {
            var sceneContainer = @event.SceneContainer;
            
        }

        public void Exit() =>
            _container.Unbind<IEventHandler<OnSceneContainerCreatedEvent<GameplayScene>>>();
    }
}