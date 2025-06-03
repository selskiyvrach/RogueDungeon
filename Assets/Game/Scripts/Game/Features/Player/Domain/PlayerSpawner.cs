using Game.Libs.Time;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.Domain
{
    public class PlayerSpawner 
    {
        private readonly IFactory<Transform, Player> _factory;
        private readonly IGameTime _time;
        private readonly Transform _parent;
        
        private Player _playerInstance;

        public PlayerSpawner(IFactory<Transform, Player> factory, IGameTime time, Transform playerParent)
        {
            _parent = playerParent;
            _factory = factory;
            _time = time;
        }

        public void SpawnPlayer()
        {
            Assert.IsNull(_playerInstance, "Player instance is already spawned.");
            _playerInstance = _factory.Create(_parent);
            _playerInstance.Initialize();
            _time.StartTicking(_playerInstance, TickOrder.Player);
        }

        public void DespawnPlayer()
        {
            _time.StopTicking(_playerInstance);
            _playerInstance.Dispose();
            _playerInstance = null;
        }
    }
}