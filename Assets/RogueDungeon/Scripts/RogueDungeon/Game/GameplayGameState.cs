using System;
using Common.Game;
using Common.GameObjectMarkers;
using Common.SceneManagement;
using RogueDungeon.Entities;
using RogueDungeon.SceneManagement;
using UniRx;
using Zenject;

namespace RogueDungeon.Game
{
    internal class GameplayGameState : IGameState
    {
        private readonly ISceneLoader _sceneLoader;

        public GameplayGameState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async void Enter() => 
            await _sceneLoader.Load<GameplayScene>();

        public void OnSceneContainerReady(DiContainer sceneContainer)
        {
            
        }
    }

    public interface ISpawner<T> where T : IGameEntity
    {
        T Spawn();
    }

    public class Spawner<T, TParent> : ISpawner<T> where T : IGameEntity where TParent : GameObjectParent  
    {
        private readonly TParent _root;
        private readonly IFactory<T> _factory;

        public Spawner(TParent root, IFactory<T> factory)
        {
            _root = root;
            _factory = factory;
        }

        public T Spawn()
        {
            var result = _factory.Create();
            result.RootTransform.SetParent(_root.transform);
            return result;
        }
    }

    public class GameplayController
    {
        private readonly ISpawner<Player.Player> _playerSpawner;

        public GameplayController(ISpawner<Player.Player> playerSpawner)
        {
            _playerSpawner = playerSpawner;
            Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ => _playerSpawner.Spawn());
        }
    }
}